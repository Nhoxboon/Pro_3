using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Enemy enemy;
    protected EnemyDataSO enemyDataSO;
    protected Core core;

    protected float startTime;
    public float StartTime => startTime;

    protected string animBoolName;

    public State(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.enemyDataSO = enemyDataSO;
        this.core = enemy.Core;
    }

    public virtual void DoChecks()
    {
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemy.EnemyCtrl.EnemyAnimation.AnimationState(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemy.EnemyCtrl.EnemyAnimation.AnimationState(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
}
