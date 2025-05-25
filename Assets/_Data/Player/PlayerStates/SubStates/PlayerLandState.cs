using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(playerAudioDataSO.landAudio);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(playerStateManager.PlayerMoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(playerStateManager.PlayerIdleState);
            }
        }
        
    }
}
