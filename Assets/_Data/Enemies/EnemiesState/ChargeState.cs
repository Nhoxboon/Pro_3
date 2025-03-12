using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingCliff;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
        isDetectingCliff = enemy.EnemyCtrl.TouchingDirection.CheckTouchingCliff();
        isDetectingWall = enemy.EnemyCtrl.TouchingDirection.CheckTouchingWall();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        enemy.SetVelocityX(enemyDataSO.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + enemyDataSO.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
