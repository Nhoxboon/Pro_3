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

    public PlayerGroundedState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
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
        isTouchingCeiling = core.TouchingDirection.IsTouchingCeiling;
    }

    public override void Enter()
    {
        base.Enter();

        playerStateManager.PlayerJumpState.ResetAmountOfJumpsLeft();
        playerStateManager.PlayerDashState.ResetCanDash();
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


        if (InputManager.Instance.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling && playerStateManager.PrimaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.ChangeState(playerStateManager.PrimaryAttackState);
        }
        else if(InputManager.Instance.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling && playerStateManager.SecondaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.ChangeState(playerStateManager.SecondaryAttackState);
        }


        else if (jumpInput && playerStateManager.PlayerJumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(playerStateManager.PlayerJumpState);
        }
        else if (!isGrounded)
        {
            playerStateManager.PlayerInAirState.StartCoyoteTime();
            stateMachine.ChangeState(playerStateManager.PlayerInAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(playerStateManager.PlayerWallGrabState);
        }
        else if (dashInput && playerStateManager.PlayerDashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(playerStateManager.PlayerDashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
