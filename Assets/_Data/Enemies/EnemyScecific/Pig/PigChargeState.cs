using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigChargeState : ChargeState
{
    private Pig pig;

    public PigChargeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
        this.pig = pig;
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

        if(!isDetectingCliff || isDetectingWall)
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
