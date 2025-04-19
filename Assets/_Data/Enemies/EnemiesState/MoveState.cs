using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected bool isDetectingWall;
    protected bool isDetectingCliff;
    protected bool isPlayerInMinAgroRange;

    public MoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = core.TouchingDirection.IsTouchingWall;
        isDetectingCliff = core.TouchingDirection.IsTouchingCliff;
        isPlayerInMinAgroRange = enemyStateManager.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityX(enemyDataSO.movementSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(enemyDataSO.movementSpeed * core.Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
