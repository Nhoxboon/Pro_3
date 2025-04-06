
using UnityEngine;

public class TargeterToProjectile : WeaponComponent
{
    protected ProjectileSpawnForWeapon projectileSpawner;
    protected Targeter targeter;
    
    protected readonly TargetsDataPackage targetsDataPackage = new TargetsDataPackage();
    
    protected void HandleSpawnProjectile(Projectile projectile)
    {
        targetsDataPackage.targets = targeter.GetTarget();

        projectile.SendDataPackage(targetsDataPackage);
    }
    
    #region Plumbing
    protected override void Start()
    {
        base.Start();

        projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadProjectileSpawnerForWeapon();
        LoadTargeter();
    }
    
    protected void LoadProjectileSpawnerForWeapon()
    {
        if(projectileSpawner != null) return;
        projectileSpawner = GetComponent<ProjectileSpawnForWeapon>();
    }
    
    protected void LoadTargeter()
    {
        if(targeter != null) return;
        targeter = GetComponent<Targeter>();
    }

    #endregion
}
