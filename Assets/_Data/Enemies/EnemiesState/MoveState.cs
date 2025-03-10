using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected bool isDetectingWall;
    protected bool isDetectingCliff;

    public MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = EnemyCtrl.Instance.TouchingDirection.CheckTouchingWall();
        isDetectingCliff = EnemyCtrl.Instance.TouchingDirection.CheckTouchingCliff();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(enemyDataSO.movementSpeed);

        isDetectingWall = EnemyCtrl.Instance.TouchingDirection.CheckTouchingWall();
        isDetectingCliff = EnemyCtrl.Instance.TouchingDirection.CheckTouchingCliff();
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
