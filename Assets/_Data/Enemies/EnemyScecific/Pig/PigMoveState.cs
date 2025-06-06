using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMoveState : MoveState
{
    private Pig pig;

    public PigMoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Pig pig) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO)
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
        else if (isDetectingWall || !isDetectingCliff)
        {
            pig.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(pig.IdleState);
        }
    }
}
