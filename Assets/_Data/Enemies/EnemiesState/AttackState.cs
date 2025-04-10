using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public AttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Transform attackPosition) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemy.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.GetAnimEvent.attackState = this;
        isAnimationFinished = false;
        core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
    
    public void SetParryWindowActive(bool value) => core.ParryReceiver.SetParryColliderActive(value);

}
