using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherRangedAttackState : RangedAttackState
{
    private Archer archer;

    public ArcherRangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, Transform attackPosition, EnemyRangedAttackStateSO stateData, Archer archer) : base(
        enemy, stateMachine, animBoolName, enemyDataSO, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
            }
            else
            {
                stateMachine.ChangeState(archer.ArcherLookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
