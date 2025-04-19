using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerStateManagerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityZero();
        playerStateManager.SetColliderHeight(playerDataSO.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        playerStateManager.SetColliderHeight(playerDataSO.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(playerStateManager.PlayerCrouchMoveState);
            }
            else if (yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(playerStateManager.PlayerIdleState);
            }
        }
    }


}
