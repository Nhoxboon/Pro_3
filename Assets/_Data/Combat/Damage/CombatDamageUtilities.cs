using System.Collections.Generic;
using UnityEngine;

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
        damageReceivers = new List<DamageReceiver>();

        foreach (var collider in colliders)
            if (TryDamage(collider.gameObject, combatDamageData, out var damageable))
            {
                damageReceivers.Add(damageable);
                return true;
            }

        return false;
    }
}