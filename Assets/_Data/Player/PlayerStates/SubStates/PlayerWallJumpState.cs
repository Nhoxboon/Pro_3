using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    protected int wallJumpDirection;

    public PlayerWallJumpState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        playerMovement.PlayerJumpState.ResetAmountOfJumpsLeft();
        playerMovement.SetVelocity(playerDataSO.wallJumpVelocity, playerDataSO.wallJumpAngle, wallJumpDirection);
        playerMovement.CheckIfShouldFlip(wallJumpDirection);
        playerMovement.PlayerJumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        PlayerCtrl.Instance.PlayerAnimation.YVelocityAnimation(playerMovement.CurrentVelocity.y);
        PlayerCtrl.Instance.PlayerAnimation.XVelocityAnimation(Mathf.Abs(playerMovement.CurrentVelocity.x));

        if (Time.time >= startTime + playerDataSO.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -playerMovement.FacingDirection;
        }
        else
        {
            wallJumpDirection = playerMovement.FacingDirection;
        }
    }
}
