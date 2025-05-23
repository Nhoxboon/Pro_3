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
            stateMachine.ChangeState(pig.MeleeAttackState);
        }
        else if (!isDetectingCliff || isDetectingWall)
        {
            stateMachine.ChangeState(pig.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(pig.DetectedPlayerState);
            else
                stateMachine.ChangeState(pig.LookForPlayerState);
        }
    }
}