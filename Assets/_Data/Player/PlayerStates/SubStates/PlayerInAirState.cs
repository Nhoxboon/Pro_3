using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected int xInput;
    protected bool jumpInput;
    protected bool jumpInputStop;
    protected bool grabInput;
    protected bool dashInput;

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    protected bool oldIsTouchingWall;
    protected bool oldIsTouchingWallBack;
    protected bool isJumping;
    protected bool isTouchingLedge;

    protected bool coyoteTime;
    protected bool wallJumpCoyoteTime;


    protected float startWallJumpCoyoteTime;

    public PlayerInAirState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = core.TouchingDirection.IsGrounded;
        isTouchingWall = core.TouchingDirection.IsTouchingWall;
        isTouchingWallBack = core.TouchingDirection.IsTouchingWallBack;
        isTouchingLedge = core.TouchingDirection.IsTouchingLedge;

        if (isTouchingWall && !isGrounded && !isTouchingLedge)
        {
            player.PlayerLedgeClimbState.SetDetectedPosition(player.transform.parent.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = InputManager.Instance.NormInputX;
        jumpInput = InputManager.Instance.JumpInput;
        jumpInputStop = InputManager.Instance.JumpInputStop;
        grabInput = InputManager.Instance.GrabInput;
        dashInput = InputManager.Instance.DashInput;

        CheckJumpMultiplier();

        if (InputManager.Instance.AttackInputs[(int)CombatInputs.primary] && player.PrimaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (InputManager.Instance.AttackInputs[(int)CombatInputs.secondary] && player.SecondaryAttackState.CanTransitionToAttackState())
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.PlayerLandState);
        }
        else if (!isTouchingLedge && isTouchingWall && !isGrounded)
        {
            stateMachine.ChangeState(player.PlayerLedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            player.PlayerWallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.PlayerWallJumpState);
        }
        else if (jumpInput && player.PlayerJumpState.CanJump())
        {
            stateMachine.ChangeState(player.PlayerJumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.PlayerWallGrabState);
        }
        else if(isTouchingWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.PlayerWallSlideState);
        }
        else if (dashInput && player.PlayerDashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.PlayerDashState);
        }
        else
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerDataSO.movementVelocity * xInput);

            PlayerCtrl.Instance.PlayerAnimation.YVelocityAnimation(core.Movement.CurrentVelocity.y);
            PlayerCtrl.Instance.PlayerAnimation.XVelocityAnimation(Mathf.Abs(core.Movement.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerDataSO.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (core.Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    protected void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerDataSO.coyoteTime)
        {
            coyoteTime = false;
            player.PlayerJumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    protected void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerDataSO.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetJumping() => isJumping = true;
}
