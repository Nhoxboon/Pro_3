using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerMovement.SetVelocityZero();
        playerMovement.SetColliderHeight(playerDataSO.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        playerMovement.SetColliderHeight(playerDataSO.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(playerMovement.PlayerCrouchMoveState);
            }
            else if (yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(playerMovement.PlayerIdleState);
            }
        }
    }


}
