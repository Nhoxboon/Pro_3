using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : ComponentDataAbstract<AttackDamage>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Damage);
    }
}
