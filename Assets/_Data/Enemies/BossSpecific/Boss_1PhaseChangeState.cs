
using UnityEngine;

public class Boss_1PhaseChangeState : PhaseChangeState
{
    private Boss_1 boss;

    public Boss_1PhaseChangeState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo, Boss_1 boss) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSo)
    {
        this.boss = boss;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isPhaseChangeTimeOver)
        {
            stateMachine.ChangeState(boss.BossMoveByPointState);
        }
    }
}
