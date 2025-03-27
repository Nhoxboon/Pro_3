using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitboxData : ComponentDataAbstract<AttackActionHitbox>
{
    public LayerMask detectedLayers;

    protected override void SetComponentDependency()
    {
        componentDependency = typeof(ActionHitbox);
    }
}
