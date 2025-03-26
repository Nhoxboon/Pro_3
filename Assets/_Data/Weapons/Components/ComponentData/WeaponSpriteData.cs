using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponSpriteData : ComponentData<AttackSprites>
{
    public WeaponSpriteData()
    {
        componentDependency = typeof(WeaponSprite);
    }
}
