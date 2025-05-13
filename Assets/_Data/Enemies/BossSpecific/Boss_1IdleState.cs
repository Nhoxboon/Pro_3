
using UnityEngine;

public class Boss_1IdleState : IdleState
{
    private Boss_1 boss;
    
    public Boss_1IdleState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.boss = boss;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(boss.BossMoveState);
        }
        else if (isIdleTimeOver)
        {
            if (Random.value < 0.5f)
                stateMachine.ChangeState(boss.BossMoveState);
            else
                stateMachine.ChangeState(boss.BossRangedAttackState);
        }
    }
}
