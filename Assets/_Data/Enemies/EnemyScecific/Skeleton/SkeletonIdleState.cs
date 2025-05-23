using UnityEngine;

public class SkeletonIdleState : IdleState
{
    private Skeleton skeleton;

    public SkeletonIdleState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Skeleton skeleton) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.skeleton = skeleton;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(skeleton.DetectedPlayerState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(skeleton.MoveState);
        }
    }
}
