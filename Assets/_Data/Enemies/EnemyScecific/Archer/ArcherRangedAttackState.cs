using UnityEngine;

public class ArcherRangedAttackState : RangedAttackState
{
    private readonly Archer archer;

    public ArcherRangedAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyRangedAttackStateSO stateData, Archer archer) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO, attackPosition, stateData)
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
            else
                stateMachine.ChangeState(archer.ArcherLookForPlayerState);
        }
    }
}