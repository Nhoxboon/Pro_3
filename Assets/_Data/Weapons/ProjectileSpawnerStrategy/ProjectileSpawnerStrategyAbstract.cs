using System;
using System.Collections;
using System.Collections.Generic;
using Nhoxboon.Projectile.Strategy;
using UnityEngine;


/*
 * The ProjectileSpawnerStrategyBase abstract class. We have a single abstract method that takes in the ProjectileSpawnInfo, 
 * the position of the spawner, the facingDirection of the spawner, the object pool to get the projectile from, 
 * and an action to invoke when a projectile is spawned.
 */
public abstract class ProjectileSpawnerStrategyAbstract
{
    public abstract void ExecuteSpawnStrategy(
        ProjectileSpawnInfo projectileSpawnInfo,
        Vector3 spawnerPos,
        int facingDirection,
        ObjectPools objectPools,
        Action<Projectile> OnSpawnProjectile
    );
}
