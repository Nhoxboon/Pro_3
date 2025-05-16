using UnityEngine;

public class Boss_1SleepState : SleepState
{
    private Boss_1 boss;
    protected bool isPlayerIn;

    public Boss_1SleepState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo, Boss_1 boss) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSo)
    {
        this.boss = boss;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerIn = boss.CheckPlayerInRoom();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerIn) stateMachine.ChangeState(boss.BossMoveState);
    }
}
