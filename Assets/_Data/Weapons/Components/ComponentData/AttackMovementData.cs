using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovementData : ComponentData<AttackMovement>
{
    public AttackMovementData()
    {
        componentDependency = typeof(WeaponMovement);
    }
}
