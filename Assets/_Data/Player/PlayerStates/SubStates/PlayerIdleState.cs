using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, EntityAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0f);
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);

        if (!isExitingState)
        {
            if (xInput != 0f)
            {
                stateMachine.ChangeState(playerStateManager.PlayerMoveState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(playerStateManager.PlayerCrouchIdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
