using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDeadState : DeadState
{
    private Archer archer;

    public ArcherDeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Archer archer) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO)
    {
        this.archer = archer;
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
