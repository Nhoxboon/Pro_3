using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public DeadState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.Instance.PlaySFX(audioDataSO.deathClip);
        enemyStateManager.gameObject.SetActive(false);
    }
}
