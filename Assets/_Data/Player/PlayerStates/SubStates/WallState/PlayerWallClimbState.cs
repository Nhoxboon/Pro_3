using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerStateManagerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            core.Movement.SetVelocityY(playerDataSO.wallClimbVelocity);

            if (yInput != 1)
            {
                stateMachine.ChangeState(playerStateManager.PlayerWallGrabState);
            }
        }
    }
}

