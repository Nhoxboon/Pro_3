using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigChargeState : ChargeState
{
    private Pig pig;

    public PigChargeState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyChargeStateSO stateData, Pig pig) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO, stateData)
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
            stateMachine.ChangeState(pig.PigMeleeAttackState);
        }
        else if (!isDetectingCliff || isDetectingWall)
        {
            stateMachine.ChangeState(pig.PigLookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(pig.PigDetectedPlayerState);
            }
            else
            {
                stateMachine.ChangeState(pig.PigLookForPlayerState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
