using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDetectedPlayerState : DetectedPlayerState
{
    private Archer archer;

    public ArcherDetectedPlayerState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Archer archer) : base(
        enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.archer = archer;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            if (Time.time >= archer.ArcherDodgeState.StartTime + archer.DodgeDataSO.dodgeCooldown)
            {
                stateMachine.ChangeState(archer.ArcherDodgeState);
            }
            else
            {
                stateMachine.ChangeState(archer.ArcherMeleeAttackState);
            }
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(archer.ArcherRangedAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(archer.ArcherLookForPlayerState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
