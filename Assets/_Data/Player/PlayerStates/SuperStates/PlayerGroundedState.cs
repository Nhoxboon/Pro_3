using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected bool jumpInput;
    protected bool isGrounded;

    public PlayerGroundedState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = PlayerCtrl.Instance.TouchingDirection.IsGrounded;
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

        if (jumpInput && playerMovement.PlayerJumpState.CanJump())
        {
            InputManager.Instance.UseJumpInput();
            stateMachine.ChangeState(playerMovement.PlayerJumpState);
        }
        else if (!isGrounded)
        {
            playerMovement.PlayerInAirState.StartCoyoteTime();
            stateMachine.ChangeState(playerMovement.PlayerInAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
