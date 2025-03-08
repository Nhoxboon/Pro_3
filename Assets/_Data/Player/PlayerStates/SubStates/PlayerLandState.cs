using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(xInput != 0)
        {
            stateMachine.ChangeState(playerMovement.PlayerMoveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(playerMovement.PlayerIdleState);
        }
    }
}
