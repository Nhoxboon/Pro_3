using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PhaseSprites
{
    [field: SerializeField] public AttackPhases Phase { get; private set; }
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}
