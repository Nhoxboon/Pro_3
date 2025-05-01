public class PigChaseState : ChaseState
{
    private readonly Pig pig;

    public PigChaseState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyChaseStateSO stateData, Pig pig) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO, stateData)
    {
        this.pig = pig;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(pig.PigMeleeAttackState);
        }
        else if (!isDetectingCliff || isDetectingWall)
        {
            stateMachine.ChangeState(pig.PigLookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(pig.PigDetectedPlayerState);
            else
                stateMachine.ChangeState(pig.PigLookForPlayerState);
        }
    }
}