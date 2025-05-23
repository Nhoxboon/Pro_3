using UnityEngine;

public class SkeletonMoveState : MoveState
{
    private Skeleton skeleton;

    public SkeletonMoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Skeleton skeleton) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.skeleton = skeleton;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(skeleton.DetectedPlayerState);
        }
        else if (isDetectingWall || !isDetectingCliff)
        {
            skeleton.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(skeleton.IdleState);
        }
    }
}
