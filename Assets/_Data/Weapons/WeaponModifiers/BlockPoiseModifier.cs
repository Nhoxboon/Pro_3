
using UnityEngine;

public class BlockPoiseModifier : Modifier<CombatPoiseData>
{
    private readonly ConditionalDelegate isBlocked;

    public BlockPoiseModifier(ConditionalDelegate isBlocked)
    {
        this.isBlocked = isBlocked;
    }
    
    public override CombatPoiseData ModifyValue(CombatPoiseData value)
    {
        if (isBlocked(value.Source.transform, out var blockDirectionInformation))
        {
            value.SetAmount(value.Amount * (1 - blockDirectionInformation.poiseAbsorption));
        }

        return value;
    }
}
