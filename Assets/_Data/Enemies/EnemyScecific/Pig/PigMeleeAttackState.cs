using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMeleeAttackState : MeleeAttackState
{
    private Pig pig;

    public PigMeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyMeleeAttackStateSO stateData, Pig pig) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO,
        audioDataSO, attackPosition, stateData)
    {
        this.pig = pig;
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
                stateMachine.ChangeState(pig.DetectedPlayerState);
            }
            else
            {
                stateMachine.ChangeState(pig.LookForPlayerState);
            }
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
