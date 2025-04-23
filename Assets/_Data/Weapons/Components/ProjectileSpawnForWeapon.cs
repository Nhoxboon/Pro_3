using System;
using UnityEngine;

public class ProjectileSpawnForWeapon : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
{
    public int chargeAmount;
    public float angleVariation;

    // Event fired off for each projectile before we call the Init() function on that projectile to allow other components to also pass through some data
    public event Action<Projectile> OnSpawnProjectile;


    protected void HandleAttackAction()
    {
        if (currentAttackData.isChargeable)
            ChargeAttack();
        else
            NormalAttack();
    }

    protected void NormalAttack()
    {
        foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
        {
            AudioManager.Instance.PlaySFX(currentAttackData.spawnSound);
            ProjectileSpawner.Instance.SpawnProjectileStrategy(
                projectileSpawnInfo,
                transform.position,
                Core.Movement.FacingDirection,
                OnSpawnProjectile
            );
        }
    }

    protected void ChargeAttack()
    {
        foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
        {
            AudioManager.Instance.PlaySFX(currentAttackData.spawnSound);
            ProjectileSpawner.Instance.SpawnWithChargeStrategy(
                projectileSpawnInfo,
                transform.position,
                Core.Movement.FacingDirection,
                chargeAmount,
                angleVariation,
                OnSpawnProjectile
            );
        }
    }

    #region Plumbing

    protected override void Start()
    {
        base.Start();

        EventHandler.OnAttackAction += HandleAttackAction;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventHandler.OnAttackAction -= HandleAttackAction;
    }


    private void OnDrawGizmosSelected()
    {
        if (data == null || !Application.isPlaying)
            return;

        foreach (var item in data.GetAllAttackData())
        foreach (var point in item.SpawnInfos)
        {
            var pos = transform.position + (Vector3)point.Offset;

            Gizmos.DrawWireSphere(pos, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pos, pos + (Vector3)point.Direction.normalized);
            Gizmos.color = Color.white;
        }
    }

    #endregion
}