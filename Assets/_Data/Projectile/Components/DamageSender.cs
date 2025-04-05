using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DamageSender : ProjectileComponent
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected bool setInactiveAfterDamage;
    [SerializeField] protected float cooldown;
    
    public UnityEvent<DamageReceiver> OnDamage;
    public UnityEvent<CombatDummy> OnCombatDummyDamage;
    public UnityEvent<RaycastHit2D> OnRaycastHit;

    protected float amount;

    protected float lastDamageTime;

    protected override void Init()
    {
        base.Init();

        lastDamageTime = Mathf.NegativeInfinity;
    }

    private void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (!Active)
            return;

        if (Time.time < lastDamageTime + cooldown)
            return;

        foreach (var hit in hits)
        {
            if (!LayerMaskUtilities.IsLayerInMask(hit, layerMask))
                continue;
            
            if (hit.collider.transform.gameObject.TryGetComponent(out CombatDummy combatDummy))
            {
                combatDummy.Damage();
                OnCombatDummyDamage?.Invoke(combatDummy);
                //NOTE: if we want to despawn the projectile we have to do in unity event because we use start coroutine to set the projectile inactive
            }

            // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
            if (!hit.collider.transform.gameObject.TryGetComponent(out DamageReceiver damageReceiver))
                continue;

            damageReceiver.Damage(new CombatDamageData(amount, projectile.gameObject));

            OnDamage?.Invoke(damageReceiver);
            OnRaycastHit?.Invoke(hit);

            lastDamageTime = Time.time;

            if (setInactiveAfterDamage) SetActive(false);

            return;
        }
    }
    
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not DamageDataPackage package)
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