using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nhoxboon.Projectile.Strategy;

public class ProjectileSpawnForWeapon : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
{
    [SerializeField] protected Transform holder;
    // Event fired off for each projectile before we call the Init() function on that projectile to allow other components to also pass through some data
    public event Action<Projectile> OnSpawnProjectile;


    // Object pool to store projectiles so we don't have to keep instantiating new ones
    private ObjectPools objectPools;

    // The strategy we use to spawn a projectile
    private ProjectileSpawnerStrategyAbstract projectileSpawnerStrategy;

    public void SetProjectileSpawnerStrategy(ProjectileSpawnerStrategyAbstract newStrategy)
    {
        projectileSpawnerStrategy = newStrategy;
    }


    //TODO: Where can put my own projectile spawner!
    // Weapon Action Animation Event is used to trigger firing the projectiles
    private void HandleAttackAction()
    {
        foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
        {
            // Spawn projectile based on the current strategy
            projectileSpawnerStrategy.ExecuteSpawnStrategy(projectileSpawnInfo, transform.position,
                Core.Movement.FacingDirection, objectPools, OnSpawnProjectile);
        }
    }

    private void SetDefaultProjectileSpawnStrategy()
    {
        // The default spawn strategy is the base ProjectileSpawnerStrategy class. It simply spawns one projectile based on the data per request
        projectileSpawnerStrategy = new ProjectileSpawnerStrategy();
    }

    protected override void HandleExit()
    {
        base.HandleExit();

        // Reset the spawner strategy every time the attack finishes in case some other component adjusted it
        SetDefaultProjectileSpawnStrategy();
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();
        objectPools = new ObjectPools(holder);

        SetDefaultProjectileSpawnStrategy();
    }

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

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
    }

    protected void LoadHolder()
    {
        if (holder != null) return;
        holder = GameObject.Find("ProjectileSpawner/Holder")?.transform;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null || !Application.isPlaying)
            return;

        foreach (var item in data.GetAllAttackData())
        {
            foreach (var point in item.SpawnInfos)
            {
                var pos = transform.position + (Vector3)point.Offset;

                Gizmos.DrawWireSphere(pos, 0.2f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(pos, pos + (Vector3)point.Direction.normalized);
                Gizmos.color = Color.white;
            }
        }
    }

    #endregion
}