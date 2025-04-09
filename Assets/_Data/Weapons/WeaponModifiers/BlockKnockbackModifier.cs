
using UnityEngine;

public class BlockKnockbackModifier : Modifier<CombatKnockbackData>
{
    private readonly ConditionalDelegate isBlocked;

    public BlockKnockbackModifier(ConditionalDelegate isBlocked)
    {
        this.isBlocked = isBlocked;
    }
    
    public override CombatKnockbackData ModifyValue(CombatKnockbackData value)
    {
        if (isBlocked(value.Source.transform, out var blockDirectionInformation))
        {
            value.Strength *= (1 - blockDirectionInformation.knockbackAbsorption);
        }

        return value;
    }
}
