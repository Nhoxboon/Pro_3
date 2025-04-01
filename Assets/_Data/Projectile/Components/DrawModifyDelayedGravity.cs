using UnityEngine;

public class DrawModifyDelayedGravity : ProjectileComponent
{
    [SerializeField] protected DelayedGravity delayedGravity;

    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not DrawModifierDataPackage drawModifierDataPackage)
            return;

        // Modify the delayed gravity distance multiplier based on draw percentage received from the weapon
        delayedGravity.distanceMultiplier = drawModifierDataPackage.DrawPercentage;
    }

    #region Plumbing

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDelayedGravity();
    }

    protected void LoadDelayedGravity()
    {
        if (delayedGravity != null) return;
        delayedGravity = GetComponent<DelayedGravity>();
        Debug.Log(transform.name + ": LoadDelayedGravity", gameObject);
    }

    #endregion
}