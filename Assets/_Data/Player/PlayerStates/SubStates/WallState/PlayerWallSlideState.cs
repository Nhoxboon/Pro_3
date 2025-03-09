using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            playerMovement.SetVelocityY(-playerDataSO.wallSlideVelocity);

            if (yInput == 0 && grabInput)
            {
                stateMachine.ChangeState(playerMovement.PlayerWallGrabState);
            }
        }  
        
    }
}
