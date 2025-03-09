using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

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

        if(!isExitingState)
        {
            playerMovement.SetVelocityX(playerDataSO.crouchMovementVelocity * playerMovement.FacingDirection);
            playerMovement.CheckIfShouldFlip(xInput);

            if (xInput == 0)
            {
                stateMachine.ChangeState(playerMovement.PlayerCrouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(playerMovement.PlayerMoveState);
            }
        }
    }
}
