using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherLookForPlayerState : LookForPlayerState
{
    private Archer archer;

    public ArcherLookForPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Archer archer) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(archer.ArcherDetectedPlayerState);
        }
        else if (isAllTurnsDone)
        {
            stateMachine.ChangeState(archer.ArcherMoveState);
        }
    }
}
