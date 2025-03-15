using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected bool isDetectingWall;
    protected bool isDetectingCliff;
    protected bool isPlayerInMinAgroRange;

    public MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = enemy.EnemyCtrl.TouchingDirection.IsTouchingWall;
        isDetectingCliff = enemy.EnemyCtrl.TouchingDirection.IsTouchingCliff;
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isDetectingWall = enemy.EnemyCtrl.TouchingDirection.IsTouchingWall;
        isDetectingCliff = enemy.EnemyCtrl.TouchingDirection.IsTouchingCliff;
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();

        enemy.SetVelocityX(enemyDataSO.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
