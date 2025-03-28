using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnerData : ComponentDataAbstract<AttackProjectileSpawner>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(ProjectileSpawnForWeapon);
    }
}
