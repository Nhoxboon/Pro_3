using UnityEngine;

public class SkeletonMeleeAttackState : MeleeAttackState
{
    private Skeleton skeleton;

    public SkeletonMeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyMeleeAttackStateSO stateData, Skeleton skeleton) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO, attackPosition, stateData)
    {
        this.skeleton = skeleton;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(skeleton.DetectedPlayerState);
            }
            else
            {
                stateMachine.ChangeState(skeleton.LookForPlayerState);
            }
        }
    }
}
