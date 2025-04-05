
using UnityEngine;
using UnityEngine.Events;

public class PoiseSender : ProjectileComponent
{
    public UnityEvent OnPoiseDamage;

    [SerializeField] protected LayerMask layerMask;
        
    private float amount;

    private void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (!Active)
            return;

        foreach (var hit in hits)
        {
            if (!LayerMaskUtilities.IsLayerInMask(hit, layerMask))
                continue;
            
            if (hit.collider.transform.gameObject.TryGetComponent(out CombatDummy combatDummy)) return;

            // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
            if (!hit.collider.transform.gameObject.TryGetComponent(out PoiseReceiver poiseDamageable))
                continue;
                
            poiseDamageable.Poise(new CombatPoiseDamageData(amount, projectile.gameObject));
                
            OnPoiseDamage?.Invoke();

            return;
        }
    }
    
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not PoiseDamageDataPackage package)
            return;

        amount = package.Amount;
    }
        
    #region Plumbing

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
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadLayerMask();
    }
    
    protected void LoadLayerMask()
    {
        if (layerMask != 0) return;
        layerMask = LayerMask.GetMask("Damageable");
        Debug.Log(transform.name + ": LoadLayerMask", gameObject);
    }

    #endregion
}
