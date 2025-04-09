
using System;
using UnityEngine;

public class DamageModifier : Modifier<CombatDamageData>
{
    public event Action<GameObject> OnModified;

    private readonly ConditionalDelegate isBlocked;

    public DamageModifier(ConditionalDelegate isBlocked)
    {
        this.isBlocked = isBlocked;
    }

    /*
     * Note: The meat and potatoes. Damage data is passed in when player gets damaged (before damage is applied). This modifier then
     * checks the angle of the attacker to the player and compares that to the block data angles. If block is successful, damage amount is modified
     * based on the DamageAbsorption field. If not successful, data is not modified.
     */
    public override CombatDamageData ModifyValue(CombatDamageData value)
    {
        if (isBlocked(value.Source.transform, out var blockDirectionInformation))
        {
            value.SetAmount(value.Amount * (1 - blockDirectionInformation.damageAbsorption));
            OnModified?.Invoke(value.Source);
        }

        return value;
    }
}
