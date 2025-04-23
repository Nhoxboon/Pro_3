
using UnityEngine;

public class ProjectileAudio : ProjectileComponent
{
    [SerializeField] protected LayerMask layerMask;
    protected AudioClip projectileAudio;
    
    private void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (!Active)
            return;

        foreach (var hit in hits)
        {
            if (!LayerMaskUtilities.IsLayerInMask(hit, layerMask))
                continue;
            AudioManager.Instance.PlaySFX(projectileAudio);
            return;
        }
    }
    
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not AudioDataPackage package) return;

        projectileAudio = package.projectileClip;
    }
    
    protected override void Awake()
    {
        base.Awake();

        projectile.ProjectileHitbox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
            
        projectile.ProjectileHitbox.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);
    }
}
