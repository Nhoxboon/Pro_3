using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : ComponentData<AttackDamage>
{
    public DamageData()
    {
        componentDependency = typeof(Damage);
    }
}
