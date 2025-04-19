using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerStateManagerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

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

        if(!isExitingState)
        {
            core.Movement.SetVelocityX(playerDataSO.crouchMovementVelocity * core.Movement.FacingDirection);
            core.Movement.CheckIfShouldFlip(xInput);

            if (xInput == 0)
            {
                stateMachine.ChangeState(playerStateManager.PlayerCrouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(playerStateManager.PlayerMoveState);
            }
        }
    }
}
