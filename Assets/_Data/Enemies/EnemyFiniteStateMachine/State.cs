using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected EnemyStateManager enemyStateManager;
    protected EnemyDataSO enemyDataSO;
    protected EnemyAudioDataSO audioDataSO;
    protected Core core;

    protected float startTime;
    public float StartTime => startTime;

    protected string animBoolName;

    public State(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo)
    {
        this.enemyStateManager = enemyStateManager;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.enemyDataSO = enemyDataSO;
        this.audioDataSO = audioDataSo;
        this.core = enemyStateManager.Core;
    }

    public virtual void DoChecks()
    {
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemyStateManager.EnemyCtrl.EnemyAnimation.AnimationState(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        enemyStateManager.EnemyCtrl.EnemyAnimation.AnimationState(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
}
