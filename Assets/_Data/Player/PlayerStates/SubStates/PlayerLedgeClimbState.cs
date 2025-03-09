using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    protected Vector2 detectedPos;
    protected Vector2 cornerPos;
    protected Vector2 startPos;
    protected Vector2 stopPos;

    protected bool isHanging;
    protected bool isClimbing;

    protected int xInput;
    protected int yInput;
    protected bool jumpInput;

    public PlayerLedgeClimbState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        PlayerCtrl.Instance.PlayerAnimation.AnimationState("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        playerMovement.SetVelocityZero();
        playerMovement.transform.parent.position = detectedPos;
        cornerPos = playerMovement.DetermineCorner();

        startPos.Set(cornerPos.x - (playerMovement.FacingDirection * playerDataSO.startOffset.x), cornerPos.y - playerDataSO.startOffset.y);
        stopPos.Set(cornerPos.x + (playerMovement.FacingDirection * playerDataSO.stopOffset.x), cornerPos.y + playerDataSO.stopOffset.y);

        playerMovement.transform.parent.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;
        if(isClimbing)
        {
            playerMovement.transform.parent.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            stateMachine.ChangeState(playerMovement.PlayerIdleState);
        }
        else
        {
            xInput = InputManager.Instance.NormInputX;
            yInput = InputManager.Instance.NormInputY;
            jumpInput = InputManager.Instance.JumpInput;

            playerMovement.SetVelocityZero();
            playerMovement.transform.parent.position = startPos;

            if (xInput == playerMovement.FacingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                PlayerCtrl.Instance.PlayerAnimation.AnimationState("climbLedge", true);
                playerMovement.transform.parent.position = stopPos;
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(playerMovement.PlayerInAirState);
            }
            else if (jumpInput && !isClimbing)
            {
                playerMovement.PlayerWallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(playerMovement.PlayerWallJumpState);
            }
        } 
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
}
