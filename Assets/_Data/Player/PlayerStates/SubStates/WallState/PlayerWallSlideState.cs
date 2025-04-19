using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerStateManagerMovement, stateMachine, playerDataSO, animBoolName)
    {
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
}
