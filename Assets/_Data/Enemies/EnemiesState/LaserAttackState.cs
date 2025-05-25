using System;
using UnityEngine;

public class LaserAttackState : AttackState
{
    protected bool isAttack;
    protected EnemyLaserAttackStateSO stateData;
    public event Action<Projectile> OnSpawnProjectile;
    
    public LaserAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyLaserAttackStateSO stateData) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isAttack = false;
        core.Movement.SetVelocityZero();
        OnSpawnProjectile += HandleDespawn;
        // AudioManager.Instance.PlaySFX(audioDataSO.chargeClip); 
    }
    
    public override void Exit()
    {
        base.Exit();
        OnSpawnProjectile -= HandleDespawn;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.laserDuration)
        {
            FinishAttack();
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        ProjectileSpawner.Instance.SpawnSingleProjectile(stateData.SpawnInfos[0],
            attackPosition.position, -core.Movement.FacingDirection, OnSpawnProjectile);
    }
    
    public void HandleDespawn(Projectile projectile)
    {
        projectile.Despawn(stateData.laserDuration - (Time.time - startTime));
    }
}
