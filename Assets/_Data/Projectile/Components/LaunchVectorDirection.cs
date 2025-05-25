
using UnityEngine;

public class LaunchVectorDirection : ProjectileComponent
{
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not DirectionDataPackage dirPackage) return;

        transform.parent.right = dirPackage.direction.normalized;
    }
}
