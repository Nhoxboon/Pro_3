using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Enemy enemy;
    protected EnemyDataSO enemyDataSO;

    protected float startTime;

    protected string animBoolName;

    public State(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.enemyDataSO = enemyDataSO;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        EnemyCtrl.Instance.EnemyAnimation.AnimationState(animBoolName, true);
    }

    public virtual void Exit()
    {
        EnemyCtrl.Instance.EnemyAnimation.AnimationState(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
    }
}
