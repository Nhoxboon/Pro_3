using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected float moveSpeed;
    
    protected bool isDetectingWall;
    protected bool isDetectingCliff;
    protected bool isPlayerInMinAgroRange;
    protected bool performCloseRangeAction;

    public MoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = core.TouchingDirection.IsTouchingWall;
        isDetectingCliff = core.TouchingDirection.IsTouchingCliff;
        isPlayerInMinAgroRange = enemyStateManager.CheckPlayerInMinAgroRange();
        performCloseRangeAction = enemyStateManager.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        moveSpeed = enemyDataSO.movementSpeed;
        core.Movement.SetVelocityX(moveSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(moveSpeed * core.Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
