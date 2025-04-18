using System;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected Projectile projectile;

    protected EnemyRangedAttackStateSO stateData;
    
    public event Action<Projectile> OnSpawnProjectile;


    public RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,
        Transform attackPosition, EnemyRangedAttackStateSO stateData) : base(enemy, stateMachine, animBoolName,
        enemyDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        
        ProjectileSpawner.Instance.SpawnProjectileStrategy(stateData.SpawnInfos[0], 
            attackPosition.position, core.Movement.FacingDirection, OnSpawnProjectile);
    }
}