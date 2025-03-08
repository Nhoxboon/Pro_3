using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected int xInput;
    protected bool isGrounded;
    protected bool jumpInput;
    protected bool jumpInputStop;
    protected bool coyoteTime;
    protected bool isJumping;

    public PlayerInAirState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        xInput = InputManager.Instance.NormInputX;
        jumpInput = InputManager.Instance.JumpInput;
        jumpInputStop = InputManager.Instance.JumpInputStop;

        CheckJumpMultiplier();

        if (isGrounded && playerMovement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(playerMovement.PlayerLandState);
        }
        else if (jumpInput && playerMovement.PlayerJumpState.CanJump())
        {
            stateMachine.ChangeState(playerMovement.PlayerJumpState);
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

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetJumping() => isJumping = true;
}
