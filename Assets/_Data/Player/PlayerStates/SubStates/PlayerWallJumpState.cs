using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    protected int wallJumpDirection;

    public PlayerWallJumpState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        player.PlayerJumpState.ResetAmountOfJumpsLeft();
        core.Movement.SetVelocity(playerDataSO.wallJumpVelocity, playerDataSO.wallJumpAngle, wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.PlayerJumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        PlayerCtrl.Instance.PlayerAnimation.YVelocityAnimation(core.Movement.CurrentVelocity.y);
        PlayerCtrl.Instance.PlayerAnimation.XVelocityAnimation(Mathf.Abs(core.Movement.CurrentVelocity.x));

        if (Time.time >= startTime + playerDataSO.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = core.Movement.FacingDirection;
        }
    }
}
