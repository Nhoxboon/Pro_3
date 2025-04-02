using UnityEngine;
using static CombatDamageUtilities; // (2)

public class Damage : WeaponComponent<DamageData, AttackDamage>
{
    [SerializeField] protected ActionHitbox hitBox;

    protected override void Start()
    {
        base.Start();
        hitBox.OnDetectedCol2D += HandleDetectCol2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCol2D -= HandleDetectCol2D;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHitBox();
    }

    protected void LoadHitBox()
    {
        if (hitBox != null) return;
        hitBox = GetComponent<ActionHitbox>();
        //Debug.Log(transform.name + " LoadHitBox", gameObject);
    }

    protected void HandleDetectCol2D(Collider2D[] detectedObjects)
    {
        // Notice that this is equal to (1), the logic has just been offloaded to a static helper class. Notice the using statement (2) is static, allowing as to call the Damage function directly instead of saying
        TryDamage(detectedObjects, new CombatDamageData(currentAttackData.Amount, Core.Root), out _);

        // (1)
        foreach (var item in detectedObjects)
            // if (item.TryGetComponent(out DamageReceiver damageReceiver))
            // {
            //     damageReceiver.Damage(new CombatDamageData(currentAttackData.Amount, Core.Root));
            // }
            if (item.TryGetComponent(out CombatDummy combatDummy))
                combatDummy.Damage();
    }
}