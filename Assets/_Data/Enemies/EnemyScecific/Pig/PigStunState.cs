using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigStunState : StunState
{
    private Pig pig;

    public PigStunState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Pig pig) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO)
    {
        this.pig = pig;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeState(pig.MeleeAttackState);
            }
            else if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(pig.ChaseState);
            }
            else
            {
                pig.LookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(pig.LookForPlayerState);
            }
        }
    }
}
