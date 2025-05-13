using UnityEngine;
using System.Collections.Generic;

public class Boss_1MoveByPointState : MoveByPointState
{
    private Boss_1 boss;
    private bool hasAttackedThisPoint;
    
    public Boss_1MoveByPointState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, List<Transform> movePoints,
        Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO,
        audioDataSO, movePoints)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        movePoints = boss.MovePoints;
        hasAttackedThisPoint = false;
    }

    public override void Exit()
    {
        base.Exit();
        enemyStateManager.currentPointIndex++;
        if (enemyStateManager.currentPointIndex >= movePoints.Count)
        {
            enemyStateManager.currentPointIndex = 0;
        }
    }

    protected override void OnReachPoint()
    {
        if (!hasAttackedThisPoint)
        {
            core.Movement.Flip();
            hasAttackedThisPoint = true;
            core.Movement.SetVelocityZero();
            stateMachine.ChangeState(boss.BossLaserAttackState);
        }
        else
        {
            base.OnReachPoint();
            hasAttackedThisPoint = false;
        }
    }
}