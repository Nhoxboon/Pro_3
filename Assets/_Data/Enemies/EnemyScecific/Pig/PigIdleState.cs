using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIdleState : IdleState
{
    private Pig pig;

    public PigIdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
        this.pig = pig;
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(pig.PigMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
