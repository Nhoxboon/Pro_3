
using UnityEngine;

public class DamageOnBlockData : ComponentDataAbstract<AttackDamage>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(DamageOnBlock);
    }
}
