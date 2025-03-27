using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackSprites : AttackData
{
    [field: SerializeField] public PhaseSprites[] PhaseSprites { get; protected set; }
}
