public class ArcherDodgeState : DodgeState
{
    private readonly Archer archer;

    public ArcherDodgeState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyDodgeStateSO stateData, Archer archer) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO, stateData)
    {
        this.archer = archer;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(audioDataSO.jumpClip);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isDodgeOver)
        {
            if (isPlayerInMaxAgroRange && performCloseRangeAction)
                stateMachine.ChangeState(archer.ArcherMeleeAttackState);
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
                stateMachine.ChangeState(archer.ArcherRangedAttackState);
            else if (!isPlayerInMaxAgroRange) stateMachine.ChangeState(archer.ArcherLookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}