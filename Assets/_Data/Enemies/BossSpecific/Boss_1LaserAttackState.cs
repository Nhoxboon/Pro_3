
using System.Collections;
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
        
        if (Time.time < startTime + stateData.chargeTime)
        {
            boss.ChargeSprite.enabled = true;
            boss.LaserWarning.EnableLaser();
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
            isAttack = true;
            boss.LaserWarning.StopLaser();
            boss.StartCoroutine(DelayedAttack());
        }
        
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(boss.BossMoveState);
        }
    }

    protected void HandleSpawnedProjectile(Projectile projectile)
    {
        Vector3 targetDirection = boss.LaserWarning.CurrentDirection.normalized;
        
        projectile.SendDataPackage(new DirectionDataPackage
        {
            direction = targetDirection
        });
    }
    
    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(0.5f);
        boss.LaserWarning.DisableLaser();
        boss.ChargeSprite.enabled = false;
        TriggerAttack();
    }

}
