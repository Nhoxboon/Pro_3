public class ArcherIdleState : IdleState
{
    private readonly Archer archer;

    public ArcherIdleState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Archer archer) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO)
    {
        this.archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
            stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
        else if (isIdleTimeOver) stateMachine.ChangeState(archer.ArcherMoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}