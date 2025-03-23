using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponSprites
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}
