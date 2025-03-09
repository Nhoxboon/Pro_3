using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerMovement.CheckIfShouldFlip(xInput);

        playerMovement.SetVelocityX(playerDataSO.movementVelocity * xInput);
        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(playerMovement.PlayerIdleState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(playerMovement.PlayerCrouchMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
