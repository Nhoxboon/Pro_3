using UnityEngine;

public class NightBorneChaseState : ChaseState
{
    private NightBorne nightBorne;

    public NightBorneChaseState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, EnemyChaseStateSO stateData,
        NightBorne nightBorne) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO,
        stateData)
    {
        this.nightBorne = nightBorne;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(nightBorne.MeleeAttackState);
        }
        else if (!isDetectingCliff || isDetectingWall)
        {
            stateMachine.ChangeState(nightBorne.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(nightBorne.DetectedPlayerState);
            else
                stateMachine.ChangeState(nightBorne.LookForPlayerState);
        }
    }
}
