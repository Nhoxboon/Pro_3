
using UnityEngine;

public class KnockbackOnParryData : ComponentDataAbstract<AttackKnockback>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(KnockbackOnParry);
    }
}
