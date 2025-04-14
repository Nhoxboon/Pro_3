
using UnityEngine;

public class DamageOnParryData : ComponentDataAbstract<AttackDamage>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(DamageOnParry);
    }
}
