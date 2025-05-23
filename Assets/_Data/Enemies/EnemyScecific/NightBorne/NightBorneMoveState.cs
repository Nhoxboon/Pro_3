using UnityEngine;

public class NightBorneMoveState : MoveState
{
    private NightBorne nightBorne;

    public NightBorneMoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, NightBorne nightBorne) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.nightBorne = nightBorne;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(nightBorne.DetectedPlayerState);
        }
        else if (isDetectingWall || !isDetectingCliff)
        {
            nightBorne.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(nightBorne.IdleState);
        }
    }
}
