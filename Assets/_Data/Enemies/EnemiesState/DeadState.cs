using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected bool isAnimationFinished;
    
    public DeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO)
    {
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityZero();
        AudioManager.Instance.PlaySFX(audioDataSO.deathClip);
        enemyStateManager.EnemyCtrl.GetAnimEvent.deadState = this;
        isAnimationFinished = false;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            enemyStateManager.gameObject.SetActive(false);
        }
    }
    
    public virtual void FinishDead() => isAnimationFinished = true;
}
