using System;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected EnemyRangedAttackStateSO stateData;


    public RangedAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO,
        Transform attackPosition, EnemyRangedAttackStateSO stateData) : base(enemyStateManager, stateMachine,
        animBoolName,
        enemyDataSO, audioDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public event Action<Projectile> OnSpawnProjectile;

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        AudioManager.Instance.PlaySFX(audioDataSO.rangedAttackClip);
        ProjectileSpawner.Instance.SpawnSingleProjectile(stateData.SpawnInfos[0],
            attackPosition.position, core.Movement.FacingDirection, OnSpawnProjectile);
    }
}