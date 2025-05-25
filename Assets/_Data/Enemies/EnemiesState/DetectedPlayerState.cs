using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPlayerState : State
{
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingCliff;

    public DetectedPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemyStateManager.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyStateManager.CheckPlayerInMaxAgroRange();
        isDetectingCliff = core.TouchingDirection.IsTouchingCliff;

        performCloseRangeAction = enemyStateManager.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);

        if (Time.time >= startTime + enemyDataSO.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
