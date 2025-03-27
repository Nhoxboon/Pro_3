using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamage : WeaponComponent<PoiseData, AttackPoise>
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
        foreach (var item in detectedObjects)
        {
            if (item.TryGetComponent(out PoiseReceiver poiseDamageable))
            {
                poiseDamageable.Poise(currentAttackData.amount);
            }
        }
    }
}
