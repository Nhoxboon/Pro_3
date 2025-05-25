using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    protected int wallJumpDirection;

    public PlayerWallJumpState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        AudioManager.Instance.PlaySFX(playerAudioDataSO.jumpAudio);
        
        playerStateManager.PlayerJumpState.ResetAmountOfJumpsLeft();
        core.Movement.SetVelocity(playerDataSO.wallJumpVelocity, playerDataSO.wallJumpAngle, wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        playerStateManager.PlayerJumpState.DecreaseAmountOfJumpsLeft();
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
