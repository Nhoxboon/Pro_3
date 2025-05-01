using UnityEngine;

public class LaunchTowardsDirection : ProjectileComponent
{
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not DirectionDataPackage dirPackage) return;
        Vector2 currentPos = transform.parent.position;
        Vector2 targetPos = dirPackage.direction;
        Vector2 dir = (targetPos - currentPos).normalized;
            
        transform.parent.right = dir;
    }
}