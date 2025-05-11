
using UnityEngine;

public class Boss_1LaserAttackState : LaserAttackState
{
    private Boss_1 boss;

    public Boss_1LaserAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine,
        string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyLaserAttackStateSO stateData, Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName,
        enemyDataSO, audioDataSO, attackPosition, stateData)
    {
        this.boss = boss;
    }
    
    public override void Enter()
    {
        base.Enter();
        if (Time.time <= startTime + stateData.chargeTime)
        {
            boss.ChargeSprite.enabled = true;
        }
        OnSpawnProjectile += HandleSpawnedProjectile;
    }
    
    public override void Exit()
    {
        base.Exit();
        OnSpawnProjectile -= HandleSpawnedProjectile;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (Time.time >= startTime + stateData.chargeTime && !isAttack)
        {
            boss.ChargeSprite.enabled = false;
            isAttack = true;
            TriggerAttack();
        }
        
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(boss.BossMoveState);
        }
    }

    protected void HandleSpawnedProjectile(Projectile projectile)
    {
        var targetDirection = boss.CheckPlayerPosition();
        
        projectile.SendDataPackage(new DirectionDataPackage
        {
            direction = targetDirection
        });
    }
}
