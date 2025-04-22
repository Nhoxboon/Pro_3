public class ArcherMoveState : MoveState
{
    private readonly Archer archer;

    public ArcherMoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Archer archer) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO)
    {
        this.archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
        }
        else if (isDetectingWall || !isDetectingCliff)
        {
            archer.ArcherIdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(archer.ArcherIdleState);
        }
    }
}