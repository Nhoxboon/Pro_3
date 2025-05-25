using UnityEngine;

public class NightBorneLookForPlayerState : LookForPlayerState
{
    private NightBorne nightBorne;

    public NightBorneLookForPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
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
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(nightBorne.MoveState);
        }
    }
}
