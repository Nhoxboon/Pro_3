using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDetectedPlayerState : DetectedpPlayerState
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

        if(performLongRangeAction)
        {
            pig.PigIdleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(pig.PigChargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
