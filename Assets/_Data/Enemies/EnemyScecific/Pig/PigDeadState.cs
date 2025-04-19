using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDeadState : DeadState
{
    private Pig pig;
    public PigDeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO)
    {
        this.pig = pig;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
