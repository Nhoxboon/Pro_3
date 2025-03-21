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

    public PlayerGroundedState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
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

        player.PlayerJumpState.ResetAmountOfJumpsLeft();
        player.PlayerDashState.ResetCanDash();
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


        if (InputManager.Instance.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if(InputManager.Instance.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }


        else if (jumpInput && player.PlayerJumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PlayerJumpState);
        }
        else if (!isGrounded)
        {
            player.PlayerInAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.PlayerInAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.PlayerWallGrabState);
        }
        else if (dashInput && player.PlayerDashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PlayerDashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
