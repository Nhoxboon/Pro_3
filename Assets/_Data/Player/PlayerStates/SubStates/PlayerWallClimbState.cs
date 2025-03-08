using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            playerMovement.SetVelocityY(playerDataSO.wallClimbVelocity);

            if (yInput != 1)
            {
                stateMachine.ChangeState(playerMovement.PlayerWallGrabState);
            }
        }
    }
}

