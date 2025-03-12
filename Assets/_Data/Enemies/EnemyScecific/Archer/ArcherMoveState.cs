using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : MoveState
{
    private Archer archer;

    public ArcherMoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Archer archer) : base(enemy, stateMachine, animBoolName, enemyDataSO)
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

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
        }
        else if (isDetectingWall || !isDetectingCliff)
        {
            archer.ArcherIdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(archer.ArcherIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
