using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDetectedPlayerState : DetectedPlayerState
{
    private Pig pig;

    public PigDetectedPlayerState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemy, stateMachine, animBoolName, enemyDataSO)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(pig.PigMeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(pig.PigChargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(pig.PigLookForPlayerState);
        }
        else if (!isDetectingCliff)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(pig.PigMoveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
