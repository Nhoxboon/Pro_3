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

    public PlayerInAirState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = PlayerCtrl.Instance.TouchingDirection.IsGrounded;
        isTouchingWall = PlayerCtrl.Instance.TouchingDirection.CheckTouchingWall();
        isTouchingWallBack = PlayerCtrl.Instance.TouchingDirection.CheckTouchingWallBack();
        isTouchingLedge = PlayerCtrl.Instance.TouchingDirection.IsTouchingLedge;

        if (isTouchingWall && !isGrounded && !isTouchingLedge)
        {
            playerMovement.PlayerLedgeClimbState.SetDetectedPosition(playerMovement.transform.parent.position);
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

        if (isGrounded && playerMovement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(playerMovement.PlayerLandState);
        }
        else if (!isTouchingLedge && isTouchingWall && !isGrounded)
        {
            stateMachine.ChangeState(playerMovement.PlayerLedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            playerMovement.PlayerWallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(playerMovement.PlayerWallJumpState);
        }
        else if (jumpInput && playerMovement.PlayerJumpState.CanJump())
        {
            stateMachine.ChangeState(playerMovement.PlayerJumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(playerMovement.PlayerWallGrabState);
        }
        else if(isTouchingWall && xInput == playerMovement.FacingDirection && playerMovement.CurrentVelocity.y <= 0f)
        {
            stateMachine.ChangeState(playerMovement.PlayerWallSlideState);
        }
        else if (dashInput && playerMovement.PlayerDashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(playerMovement.PlayerDashState);
        }
        else
        {
            playerMovement.CheckIfShouldFlip(xInput);
            playerMovement.SetVelocityX(playerDataSO.movementVelocity * xInput);

            PlayerCtrl.Instance.PlayerAnimation.YVelocityAnimation(playerMovement.CurrentVelocity.y);
            PlayerCtrl.Instance.PlayerAnimation.XVelocityAnimation(Mathf.Abs(playerMovement.CurrentVelocity.x));
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
                playerMovement.SetVelocityY(playerMovement.CurrentVelocity.y * playerDataSO.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (playerMovement.CurrentVelocity.y <= 0f)
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
            playerMovement.PlayerJumpState.DecreaseAmountOfJumpsLeft();
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
