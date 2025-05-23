using UnityEngine;

public class SkeletonChaseState : ChaseState
{
    private Skeleton skeleton;

    public SkeletonChaseState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyChaseStateSO stateData, Skeleton skeleton) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO, stateData)
    {
        this.skeleton = skeleton;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(skeleton.MeleeAttackState);
        }
        else if (!isDetectingCliff || isDetectingWall)
        {
            stateMachine.ChangeState(skeleton.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(skeleton.DetectedPlayerState);
            else
                stateMachine.ChangeState(skeleton.LookForPlayerState);
        }
    }
}
