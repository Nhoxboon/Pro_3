using UnityEngine;

public class NightBorneDeadState : DeadState
{
    private NightBorne nightBorne;

    public NightBorneDeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, NightBorne nightBorne) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.nightBorne = nightBorne;
    }
}
