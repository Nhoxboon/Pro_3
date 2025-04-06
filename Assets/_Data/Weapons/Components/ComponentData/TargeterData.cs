
using UnityEngine;

public class TargeterData : ComponentDataAbstract<AttackTargeter>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Targeter);
    }
}
