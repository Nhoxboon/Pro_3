using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;

    public DodgeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
        isGrounded = core.TouchingDirection.IsGrounded;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        core.Movement.SetVelocity(enemyDataSO.dodgeSpeed, enemyDataSO.dodgeAngle, -core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + enemyDataSO.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
