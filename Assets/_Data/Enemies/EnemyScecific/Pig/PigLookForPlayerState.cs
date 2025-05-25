using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigLookForPlayerState : LookForPlayerState
{
    private Pig pig;

    public PigLookForPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Pig pig) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.pig = pig;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(pig.DetectedPlayerState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(pig.MoveState);
        }
    }
}
