
using System.Collections.Generic;
using UnityEngine;

public class Boss_1RangedAttackState : RangedAttackState
{
    private Boss_1 boss;

    public Boss_1RangedAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyRangedAttackStateSO stateData, Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO, attackPosition, stateData)
    {
        this.boss = boss;
    }
    
    public override void Enter()
    {
        base.Enter();
        OnSpawnProjectile += HandleSpawnedProjectile;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isAnimationFinished)
        {
            if (enemyStateManager.currentPointIndex != 0)
            {
                stateMachine.ChangeState(boss.BossMoveByPointState);
            }
            else
            {
                stateMachine.ChangeState(boss.BossMoveState);
            }
        }
    }
    
    protected void HandleSpawnedProjectile(Projectile projectile)
    {
        var targetDirection = PlayerCtrl.Instance.transform.position;

        projectile.SendDataPackage(new DirectionDataPackage
        {
            direction = targetDirection
        });
    }

    public override void Exit()
    {
        base.Exit();
        OnSpawnProjectile -= HandleSpawnedProjectile;
    }
}
