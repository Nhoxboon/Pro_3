using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseData : ComponentDataAbstract<AttackPoise>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(PoiseDamage);
    }
}
