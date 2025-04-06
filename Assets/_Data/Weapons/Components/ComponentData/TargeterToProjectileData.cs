
using UnityEngine;

public class TargeterToProjectileData : ComponentDataAbstract
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(TargeterToProjectile);
    }
}
