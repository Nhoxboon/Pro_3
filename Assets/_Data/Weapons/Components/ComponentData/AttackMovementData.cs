using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovementData : ComponentDataAbstract<AttackMovement>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(WeaponMovement);
    }
}
