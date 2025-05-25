
using UnityEngine;

public class PhaseChangeState : State
{
    protected BossDataSO bossDataSO;
    protected bool isPhaseChangeTimeOver;
    public bool IsPhaseChangeTimeOver => isPhaseChangeTimeOver;
    
    public PhaseChangeState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSo)
    {
        this.bossDataSO = enemyDataSO as BossDataSO;
    }


    public override void Enter()
    {
        base.Enter();
        core.Stats.Health.IsInvincible = true;
        isPhaseChangeTimeOver = false;
        core.Movement.SetVelocityX(0f);
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(Time.time >= startTime + bossDataSO.phaseChangeTime)
        {
            isPhaseChangeTimeOver = true;
        }
    }
    
    public override void Exit()
    {
        base.Exit();
        core.Stats.Health.IsInvincible = false;
    }
}
