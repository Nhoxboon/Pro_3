using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponSpriteData : ComponentDataAbstract<AttackSprites>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(WeaponSprite);
    }
}
