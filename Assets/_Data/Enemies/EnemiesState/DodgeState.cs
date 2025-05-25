using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected float dodgeSpeed;
    
    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    
    protected EnemyDodgeStateSO stateData;

    public DodgeState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyDodgeStateSO stateData) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = enemyStateManager.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = enemyStateManager.CheckPlayerInMaxAgroRange();
        isGrounded = core.TouchingDirection.IsGrounded;
    }

    public override void Enter()
    {
        base.Enter();
        
        dodgeSpeed = stateData.dodgeSpeed;

        isDodgeOver = false;
        core.Movement.SetVelocity(dodgeSpeed, stateData.dodgeAngle, -core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
