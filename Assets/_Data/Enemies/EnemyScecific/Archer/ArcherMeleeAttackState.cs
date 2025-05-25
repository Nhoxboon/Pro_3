using UnityEngine;

public class ArcherMeleeAttackState : MeleeAttackState
{
    private readonly Archer archer;

    public ArcherMeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyMeleeAttackStateSO stateData, Archer archer) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO, attackPosition, stateData)
    {
        this.archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
            else if (!isPlayerInMinAgroRange) stateMachine.ChangeState(archer.ArcherLookForPlayerState);
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}