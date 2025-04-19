using System;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected EnemyRangedAttackStateSO stateData;
    
    public event Action<Projectile> OnSpawnProjectile;


    public RangedAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,
        Transform attackPosition, EnemyRangedAttackStateSO stateData) : base(enemyStateManager, stateMachine, animBoolName,
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