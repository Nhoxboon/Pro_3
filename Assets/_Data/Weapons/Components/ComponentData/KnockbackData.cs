using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackData : ComponentDataAbstract<AttackKnockback>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Knockback);

    }
}
