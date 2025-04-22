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

    public StunState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.TouchingDirection.IsGrounded;
        performCloseRangeAction = enemyStateManager.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = enemyStateManager.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementStop = false;
    }

    public override void Exit()
    {
        base.Exit();

        enemyStateManager.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > startTime + enemyDataSO.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + enemyDataSO.stunKnockBackTime && !isMovementStop)
        {
            isMovementStop = true;
            core.Movement.SetVelocityX(0f);
        }
    }
}
