using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigStunState : StunState
{
    private Pig pig;
    public PigStunState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO)
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

        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeState(pig.PigMeleeAttackState);
            }
            else if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(pig.PigChargeState);
            }
            else
            {
                pig.PigLookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(pig.PigLookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
