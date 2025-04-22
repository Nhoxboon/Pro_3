using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherRangedAttackState : RangedAttackState
{
    private Archer archer;

    public ArcherRangedAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition, EnemyRangedAttackStateSO stateData, Archer archer) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO, attackPosition, stateData)
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
