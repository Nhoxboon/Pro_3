using UnityEngine;

public class NightBorneMeleeAttackState : MeleeAttackState
{
    private NightBorne nightBorne;

    public NightBorneMeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyMeleeAttackStateSO stateData, NightBorne nightBorne) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO, attackPosition, stateData)
    {
        this.nightBorne = nightBorne;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(nightBorne.DetectedPlayerState);
            }
            else
            {
                stateMachine.ChangeState(nightBorne.LookForPlayerState);
            }
        }
    }
}
