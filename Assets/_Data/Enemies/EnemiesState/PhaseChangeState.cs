
using UnityEngine;

public class PhaseChangeState : State
{
    protected BossDataSO bossDataSO;
    protected bool isPhaseChangeTimeOver;
    
    public PhaseChangeState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSo)
    {
        this.bossDataSO = enemyDataSO as BossDataSO;
    }


    public override void Enter()
    {
        base.Enter();
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
}
