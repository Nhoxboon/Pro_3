using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMeleeAttackState : MeleeAttackState
{
    private Pig pig;

    public PigMeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Transform attackPosition, EnemyMeleeAttackStateSO stateData, Pig pig) : base(enemy, stateMachine, animBoolName, enemyDataSO, attackPosition, stateData)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
