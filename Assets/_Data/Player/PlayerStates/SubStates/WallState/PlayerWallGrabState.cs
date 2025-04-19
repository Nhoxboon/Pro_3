using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    protected Vector2 holdPosition;

    public PlayerWallGrabState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerStateManagerMovement, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = playerStateManager.transform.position;
        HoldPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            HoldPosition();

            if (yInput > 0)
            {
                stateMachine.ChangeState(playerStateManager.PlayerWallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(playerStateManager.PlayerWallSlideState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected void HoldPosition()
    {
        playerStateManager.transform.position = holdPosition;

        core.Movement.SetVelocityX(0f);
        core.Movement.SetVelocityY(0f);
    }
}
