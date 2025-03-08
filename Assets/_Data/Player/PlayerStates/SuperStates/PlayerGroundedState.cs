using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool jumpInput;
    protected bool grabInput;
    protected bool isGrounded;
    protected bool isTouchingWall;

    public PlayerGroundedState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
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

        playerMovement.PlayerJumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManager.Instance.NormInputX;
        jumpInput = InputManager.Instance.JumpInput;
        grabInput = InputManager.Instance.GrabInput;

        if (jumpInput && playerMovement.PlayerJumpState.CanJump())
        {
            stateMachine.ChangeState(playerMovement.PlayerJumpState);
        }
        else if (!isGrounded)
        {
            playerMovement.PlayerInAirState.StartCoyoteTime();
            stateMachine.ChangeState(playerMovement.PlayerInAirState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(playerMovement.PlayerWallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
