using System.Collections.Generic;
using UnityEngine;


/*
 * This Utility class provides some static functions for logic we might perform in many different places. This way
 * we can keep it all consolidated here and only have to change it in one place. That is the dream anyway.
 *
 * For example: The Damage functions are called by both DamageOnHitBoxAction and DamageOnBlock weapon components.
 */
public static class CombatDamageUtilities
{
    public static bool TryDamage(GameObject gameObject, CombatDamageData combatDamageData,
        out DamageReceiver damageReceiver)
    {
        // TryGetComponentInChildren is a custom GameObject extension method.
        if (gameObject.TryGetComponentInChildren(out damageReceiver))
        {
            damageReceiver.Damage(combatDamageData);
            return true;
        }

        return false;
    }

    public static bool TryDamage(Collider2D[] colliders, CombatDamageData combatDamageData,
        out List<DamageReceiver> damageReceivers)
    {
        var hasDamaged = false;
        damageReceivers = new List<DamageReceiver>();

        foreach (var collider in colliders)
            if (TryDamage(collider.gameObject, combatDamageData, out var damageable))
            {
                damageReceivers.Add(damageable);
                hasDamaged = true;
            }

        return hasDamaged;
    }
}