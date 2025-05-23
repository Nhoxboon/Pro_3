using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDeadState : DeadState
{
    private Pig pig;

    public PigDeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Pig pig) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO)
    {
        this.pig = pig;
    }
}
