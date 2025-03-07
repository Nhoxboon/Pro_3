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

    public PlayerTouchingWallState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = PlayerCtrl.Instance.TouchingDirection.IsGrounded;
        isTouchingWall = PlayerCtrl.Instance.TouchingDirection.IsTouchingWall;
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
            playerMovement.PlayerWallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(playerMovement.PlayerWallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(playerMovement.PlayerIdleState);
        }
        else if (!isTouchingWall || (xInput != playerMovement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(playerMovement.PlayerInAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
