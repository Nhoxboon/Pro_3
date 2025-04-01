using UnityEngine;
using UnityEngine.Events;

public class DamageSender : ProjectileComponent
{
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public bool SetInactiveAfterDamage { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    public UnityEvent<DamageReceiver> OnDamage;
    public UnityEvent<RaycastHit2D> OnRaycastHit;

    private float amount;

    private float lastDamageTime;

    protected override void Init()
    {
        base.Init();

        lastDamageTime = Mathf.NegativeInfinity;
    }

    private void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (!Active)
            return;

        if (Time.time < lastDamageTime + Cooldown)
            return;

        foreach (var hit in hits)
        {
            // Is the object under consideration part of the LayerMask that we can damage?
            if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                continue;
            
            // If the object is a CombatDummy, we want to deal damage to it and then despawn the projectile
            if (hit.collider.transform.gameObject.TryGetComponent(out CombatDummy combatDummy))
            {
                combatDummy.Damage(10);
                ProjectileSpawner.Instance.Despawn(projectile.gameObject);
            }

            // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
            if (!hit.collider.transform.gameObject.TryGetComponent(out DamageReceiver damageReceiver))
                continue;

            damageReceiver.Damage(new CombatDamageData(amount, projectile.gameObject));

            OnDamage?.Invoke(damageReceiver);
            OnRaycastHit?.Invoke(hit);

            lastDamageTime = Time.time;

            if (SetInactiveAfterDamage) SetActive(false);

            return;
        }
    }

    // Handles checking to see if the data is relevant or not, and if so, extracts the information we care about
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

        projectile.ProjectileImpact.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        projectile.ProjectileImpact.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);
    }

    #endregion
}