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

    public PlayerTouchingWallState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
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
            player.PlayerLedgeClimbState.SetDetectedPosition(player.transform.parent.position);
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
            player.PlayerWallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.PlayerWallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }
        else if (!isTouchingWall || (xInput != core.Movement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.PlayerInAirState);
        }
        else if(isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.PlayerLedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
