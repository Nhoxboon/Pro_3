using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;

    protected float idleTime;

    public IdleState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = enemyStateManager.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            core.Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    protected void SetRandomIdleTime()
    {
        idleTime = Random.Range(enemyDataSO.minIdleTime, enemyDataSO.maxIdleTime);
    }
}
