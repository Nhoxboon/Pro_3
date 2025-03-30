using UnityEngine;

/*
 * The DamageData class holds information we want to pass through the IDamageable interface.
 */
public class CombatDamageData
{
    public CombatDamageData(float amount, GameObject source)
    {
        Amount = amount;
        Source = source;
    }

    public float Amount { get; private set; }
    public GameObject Source { get; private set; }

    public void SetAmount(float amount)
    {
        Amount = amount;
    }
}