using UnityEngine;

public class NightBorneDetectedPlayerState : DetectedPlayerState
{
    private NightBorne nightBorne;

    public NightBorneDetectedPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, NightBorne nightBorne) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
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
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(nightBorne.ChaseState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(nightBorne.LookForPlayerState);
        }
        else if (!isDetectingCliff)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(nightBorne.MoveState);
        }
    }
}
