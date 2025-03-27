using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHoldData : ComponentDataAbstract
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(InputHold);
    }
}
