using UnityEngine;

public class SkeletonDetectedPlayer : DetectedPlayerState
{
    private Skeleton skeleton;

    public SkeletonDetectedPlayer(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Skeleton skeleton) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
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
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(skeleton.ChaseState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(skeleton.LookForPlayerState);
        }
        else if (!isDetectingCliff)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(skeleton.MoveState);
        }
    }
}
