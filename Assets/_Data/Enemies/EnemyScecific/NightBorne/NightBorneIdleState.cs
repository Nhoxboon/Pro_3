using UnityEngine;

public class NightBorneIdleState : IdleState
{
    private NightBorne nightBorne;

    public NightBorneIdleState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, NightBorne nightBorne) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.nightBorne = nightBorne;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(nightBorne.DetectedPlayerState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(nightBorne.MoveState);
        }
    }
}
