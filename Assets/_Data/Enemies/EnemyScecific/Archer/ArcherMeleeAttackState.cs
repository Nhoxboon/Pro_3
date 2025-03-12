using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMeleeAttackState : MeleeAttackState
{
    private Archer archer;

    public ArcherMeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Transform attackPosition, Archer archer) : base(enemy, stateMachine, animBoolName, enemyDataSO, attackPosition)
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
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
            }
            else if(!isPlayerInMinAgroRange)
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
