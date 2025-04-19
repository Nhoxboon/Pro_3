using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerStateManagerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(xInput);

        core.Movement.SetVelocityX(playerDataSO.movementVelocity * xInput);
        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(playerStateManager.PlayerIdleState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(playerStateManager.PlayerCrouchMoveState);
            }
        }
    }
}
