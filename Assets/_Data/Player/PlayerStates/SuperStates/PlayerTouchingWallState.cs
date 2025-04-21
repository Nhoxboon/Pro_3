using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected int xInput;
    protected int yInput;
    protected bool jumpInput;
    protected bool grabInput;
    protected bool isTouchingLedge;

    public PlayerTouchingWallState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, EntityAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.TouchingDirection.IsGrounded;
        isTouchingWall = core.TouchingDirection.IsTouchingWall;
        isTouchingLedge = core.TouchingDirection.IsTouchingLedge;

        if (isTouchingWall && !isTouchingLedge)
        {
            playerStateManager.PlayerLedgeClimbState.SetDetectedPosition(playerStateManager.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManager.Instance.NormInputX;
        yInput = InputManager.Instance.NormInputY;
        grabInput = InputManager.Instance.GrabInput;
        jumpInput = InputManager.Instance.JumpInput;

        if (jumpInput)
        {
            playerStateManager.PlayerWallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(playerStateManager.PlayerWallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(playerStateManager.PlayerIdleState);
        }
        else if (!isTouchingWall || (xInput != core.Movement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(playerStateManager.PlayerInAirState);
        }
        else if(isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(playerStateManager.PlayerLedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
