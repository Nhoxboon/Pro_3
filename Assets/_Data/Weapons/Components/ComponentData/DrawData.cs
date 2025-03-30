using UnityEngine;

public class DrawData : ComponentDataAbstract<AttackDraw>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Draw);
    }
}
