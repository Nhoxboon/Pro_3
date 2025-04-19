using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherLookForPlayerState : LookForPlayerState
{
    private Archer archer;

    public ArcherLookForPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Archer archer) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
        }
        else if (isAllTurnsDone)
        {
            stateMachine.ChangeState(archer.ArcherMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
