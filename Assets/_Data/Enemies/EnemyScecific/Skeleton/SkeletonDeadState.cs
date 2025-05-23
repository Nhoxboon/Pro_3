using UnityEngine;

public class SkeletonDeadState : DeadState
{
    private Skeleton skeleton;

    public SkeletonDeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Skeleton skeleton) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.skeleton = skeleton;
    }
}
