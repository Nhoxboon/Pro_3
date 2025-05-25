using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFXLoop(playerStateManager.PlayerAudioDataSO.wallSlideAudio);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            core.Movement.SetVelocityY(-playerDataSO.wallSlideVelocity);

            if (yInput == 0 && grabInput)
            {
                stateMachine.ChangeState(playerStateManager.PlayerWallGrabState);
            }
        }  
    }
    
    public override void Exit()
    {
        base.Exit();
        AudioManager.Instance.StopSFXLoop();
    }
}
