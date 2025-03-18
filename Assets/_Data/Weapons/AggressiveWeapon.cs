using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    [SerializeField] protected AggressiveWeaponDataSO aggressiveWeaponDataSO;

    [SerializeField] protected List<DamageReceiver> detectedDamageables = new List<DamageReceiver>();
    [SerializeField] protected List<Knockbackable> detectedKnockbackables = new List<Knockbackable>();
    [SerializeField] protected List<CombatDummy> detectedDummyDamageables = new List<CombatDummy>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponDataSO.GetType() == typeof(AggressiveWeaponDataSO))
        {
            aggressiveWeaponDataSO = (AggressiveWeaponDataSO)weaponDataSO;
        }
        else
        {
            Debug.LogError("Wrong data for " + transform.name);
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    protected void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponDataSO.AttackDetails[attackCounter];

        foreach(DamageReceiver item in detectedDamageables.ToList())
        {
            item.Damage(details.damageAmount);
        }

        foreach(Knockbackable item in detectedKnockbackables.ToList())
        {
            item.Knockback(details.knockbackAngle, details.knockbackStrength, core.Movement.FacingDirection);
        }

        foreach (CombatDummy item in detectedDummyDamageables.ToList())
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        DamageReceiver damageable = collision.GetComponent<DamageReceiver>();

        if (damageable != null)
        {
            detectedDamageables.Add(damageable);
        }

        Knockbackable knockbackable = collision.GetComponent<Knockbackable>();

        if (knockbackable != null)
        {
            detectedKnockbackables.Add(knockbackable);
        }

        CombatDummy combatDummy = collision.GetComponent<CombatDummy>();

        if (combatDummy != null)
        {
            detectedDummyDamageables.Add(combatDummy);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        DamageReceiver damageable = collision.GetComponent<DamageReceiver>();

        if (damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }

        Knockbackable knockbackable = collision.GetComponent<Knockbackable>();

        if (knockbackable != null)
        {
            detectedKnockbackables.Remove(knockbackable);
        }

        CombatDummy combatDummy = collision.GetComponent<CombatDummy>();

        if (combatDummy != null)
        {
            detectedDummyDamageables.Remove(combatDummy);
        }
    }
}
