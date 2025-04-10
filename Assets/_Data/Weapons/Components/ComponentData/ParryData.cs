
using UnityEngine;

public class ParryData : ComponentDataAbstract<AttackParry>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Parry);
    }
}
