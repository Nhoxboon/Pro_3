using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    [SerializeField] protected AggressiveWeaponDataSO aggressiveWeaponDataSO;

    [SerializeField] protected List<IDamageable> detectedDamageable = new List<IDamageable>();

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

        foreach(IDamageable item in detectedDamageable)
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
    }
}
