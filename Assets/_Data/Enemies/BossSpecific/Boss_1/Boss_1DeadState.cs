
using UnityEngine;

public class Boss_1DeadState : DeadState
{
    private Boss_1 boss;
    
    public Boss_1DeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.boss = boss;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            boss.BossHealthBarUI.gameObject.SetActive(false);
            OnDead?.Invoke();
            GameObject.Find("UI").GetComponent<UI>().SwitchToEndGame();
        }
    }
}
