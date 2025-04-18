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

    protected EnemyChargeStateSO stateData;

    public ChargeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyChargeStateSO stateData) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
        isDetectingCliff = core.TouchingDirection.IsTouchingCliff;
        isDetectingWall = core.TouchingDirection.IsTouchingWall;
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        core.Movement.SetVelocityX(stateData.chargeSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(stateData.chargeSpeed * core.Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
