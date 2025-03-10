using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMoveState : MoveState
{
    private Pig pig;

    public PigMoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Pig pig) : base(enemy, stateMachine, animBoolName, enemyDataSO)
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

        if (isDetectingWall || !isDetectingCliff)
        {
            pig.PigIdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(pig.PigIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
