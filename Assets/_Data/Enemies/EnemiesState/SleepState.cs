using UnityEngine;

public class SleepState : State
{
    public SleepState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSo) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSo)
    {
    }


}
