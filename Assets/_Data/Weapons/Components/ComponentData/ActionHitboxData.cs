using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitboxData : ComponentData<AttackActionHitbox>
{
    public LayerMask detectedLayers;

    public ActionHitboxData()
    {
        componentDependency = typeof(ActionHitbox);
    }
}
