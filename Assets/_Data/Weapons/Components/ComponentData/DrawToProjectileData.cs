using UnityEngine;

public class DrawToProjectileData : ComponentDataAbstract
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(DrawToProjectile);
    }
}
