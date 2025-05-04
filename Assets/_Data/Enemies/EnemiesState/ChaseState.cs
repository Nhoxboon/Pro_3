using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    protected float chaseSpeed;
    
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingCliff;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    protected EnemyChaseStateSO stateData;

    public ChaseState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyChaseStateSO stateData) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemyStateManager.CheckPlayerInMinAgroRange();
        isDetectingCliff = core.TouchingDirection.IsTouchingCliff;
        isDetectingWall = core.TouchingDirection.IsTouchingWall;
        performCloseRangeAction = enemyStateManager.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        chaseSpeed = stateData.chaseSpeed;

        isChargeTimeOver = false;
        core.Movement.SetVelocityX(chaseSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(chaseSpeed * core.Movement.FacingDirection);

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
