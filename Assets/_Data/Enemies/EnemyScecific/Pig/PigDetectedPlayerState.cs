using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDetectedPlayerState : DetectedPlayerState
{
    private Pig pig;

    public PigDetectedPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Pig pig) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.pig = pig;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(pig.MeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(pig.ChaseState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(pig.LookForPlayerState);
        }
        else if (!isDetectingCliff)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(pig.MoveState);
        }
    }

}
