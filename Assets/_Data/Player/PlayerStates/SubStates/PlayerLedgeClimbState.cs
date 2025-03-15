using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    protected Vector2 detectedPos;
    protected Vector2 cornerPos;
    protected Vector2 startPos;
    protected Vector2 stopPos;
    protected Vector2 workSpace;

    protected bool isHanging;
    protected bool isClimbing;
    protected bool isTouchingCeiling;

    protected int xInput;
    protected int yInput;
    protected bool jumpInput;

    public PlayerLedgeClimbState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
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

        core.Movement.SetVelocityZero();
        player.transform.parent.position = detectedPos;
        cornerPos = DetermineCorner();

        startPos.Set(cornerPos.x - (core.Movement.FacingDirection * playerDataSO.startOffset.x), cornerPos.y - playerDataSO.startOffset.y);
        stopPos.Set(cornerPos.x + (core.Movement.FacingDirection * playerDataSO.stopOffset.x), cornerPos.y + playerDataSO.stopOffset.y);

        player.transform.parent.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;
        if(isClimbing)
        {
            player.transform.parent.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (isAnimationFinished)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.PlayerCrouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.PlayerIdleState);
            }
        }
        else
        {
            xInput = InputManager.Instance.NormInputX;
            yInput = InputManager.Instance.NormInputY;
            jumpInput = InputManager.Instance.JumpInput;

            core.Movement.SetVelocityZero();
            player.transform.parent.position = startPos;

            if (xInput == core.Movement.FacingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                PlayerCtrl.Instance.PlayerAnimation.AnimationState("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.PlayerInAirState);
            }
            else if (jumpInput && !isClimbing)
            {
                player.PlayerWallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.PlayerWallJumpState);
            }
        } 
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    public void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * core.Movement.FacingDirection * 0.015f), Vector2.up, playerDataSO.standColliderHeight, core.TouchingDirection.WhatIsGround);

        PlayerCtrl.Instance.PlayerAnimation.AnimationState("isTouchingCeiling", isTouchingCeiling);
    }

    public Vector2 DetermineCorner()
    {
        return core.TouchingDirection.DetermineLedgePos(workSpace, core.Movement.FacingDirection);
    }
}
