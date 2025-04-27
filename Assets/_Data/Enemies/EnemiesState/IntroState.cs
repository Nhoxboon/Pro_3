
using UnityEngine;

public class IntroState : State
{
    protected bool isAnimationFinished;
    
    public IntroState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSo)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        enemyStateManager.EnemyCtrl.GetAnimEvent.introState = this;
        isAnimationFinished = false;
        core.Movement.SetVelocityX(0f);
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);
    }
    
    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }
}
