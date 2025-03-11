using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigLookForPlayerState : LookForPlayerState
{
    private Pig pig;

    public PigLookForPlayerState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemy, stateMachine, animBoolName, enemyDataSO)
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

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(pig.PigDetectedPlayerState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(pig.PigMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
