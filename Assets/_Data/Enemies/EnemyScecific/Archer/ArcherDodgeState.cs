using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDodgeState : DodgeState
{
    private Archer archer;

    public ArcherDodgeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyDodgeStateSO stateData, Archer archer) : base(enemy, stateMachine, animBoolName, enemyDataSO, stateData)
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

        if (isDodgeOver)
        {
            if (isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(archer.ArcherMeleeAttackState);
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(archer.ArcherRangedAttackState);
            }
            else if(!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(archer.ArcherLookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
