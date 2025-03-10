using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    protected bool jumpInput;
    protected bool grabInput;
    protected bool dashInput;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected bool isTouchingCeiling;

    public PlayerGroundedState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = PlayerCtrl.Instance.TouchingDirection.IsGrounded;
        isTouchingWall = PlayerCtrl.Instance.TouchingDirection.CheckTouchingWall();
        isTouchingLedge = PlayerCtrl.Instance.TouchingDirection.IsTouchingLedge;
        isTouchingCeiling = PlayerCtrl.Instance.TouchingDirection.IsTouchingCeiling;
    }

    public override void Enter()
    {
        base.Enter();

        playerMovement.PlayerJumpState.ResetAmountOfJumpsLeft();
        playerMovement.PlayerDashState.ResetCanDash();
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
        jumpInput = InputManager.Instance.JumpInput;
        grabInput = InputManager.Instance.GrabInput;
        dashInput = InputManager.Instance.DashInput;

        if (jumpInput && playerMovement.PlayerJumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(playerMovement.PlayerJumpState);
        }
        else if (!isGrounded && isTouchingCeiling && !isTouchingCeiling)
        {
            playerMovement.PlayerInAirState.StartCoyoteTime();
            stateMachine.ChangeState(playerMovement.PlayerInAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(playerMovement.PlayerWallGrabState);
        }
        else if (dashInput && playerMovement.PlayerDashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(playerMovement.PlayerDashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
