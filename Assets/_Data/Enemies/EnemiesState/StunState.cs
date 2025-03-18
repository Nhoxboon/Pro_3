using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStop;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    public StunState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.TouchingDirection.IsGrounded;
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementStop = false;
        core.Movement.SetVelocity(enemyDataSO.stunKnockBackSpeed, enemyDataSO.stunKnockBackAngle, enemy.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time > startTime + enemyDataSO.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + enemyDataSO.stunKnockBackTime && !isMovementStop)
        {
            isMovementStop = true;
            core.Movement.SetVelocityX(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
