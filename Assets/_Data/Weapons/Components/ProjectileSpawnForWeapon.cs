using System;
using UnityEngine;

public class ProjectileSpawnForWeapon : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
{
    // Event fired off for each projectile before we call the Init() function on that projectile to allow other components to also pass through some data
    public event Action<Projectile> OnSpawnProjectile;


    private void HandleAttackAction()
    {
        if (currentAttackData.isChargeable)
        {
            Debug.Log("Chargeable attack detected");
        }
        else
        {
            foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
                ProjectileSpawner.Instance.SpawnProjectileStrategy(
                    projectileSpawnInfo,
                    transform.position,
                    Core.Movement.FacingDirection,
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