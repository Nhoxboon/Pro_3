using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public Action OnDead;
    
    protected bool isAnimationFinished;
    
    public DeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.Instance.PlaySFX(audioDataSO.deathClip);
        enemyStateManager.EnemyCtrl.GetAnimEvent.deadState = this;
        isAnimationFinished = false;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();
        if (isAnimationFinished)
        {
            enemyStateManager.gameObject.SetActive(false);
            core.Death.Die();
        }
    }
    
    public virtual void FinishDead() => isAnimationFinished = true;
}
