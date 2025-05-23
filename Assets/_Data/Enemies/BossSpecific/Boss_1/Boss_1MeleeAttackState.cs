
using UnityEngine;

public class Boss_1MeleeAttackState : MeleeAttackState
{
    private Boss_1 boss;

    public Boss_1MeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyMeleeAttackStateSO stateData, Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO,
        audioDataSO, attackPosition, stateData)
    {
        this.boss = boss;
    }
    
    public override void Enter()
    {
        base.Enter();

        if (boss.IsPhaseChange)
        {
            attackDamage = stateData.attackDamage * 2;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isAnimationFinished)
        {
            if (boss.IsPhaseChange && Random.value < 0.3f)
                stateMachine.ChangeState(boss.MoveByPointState);
            else
                stateMachine.ChangeState(boss.MoveState);
        }
    }
}
