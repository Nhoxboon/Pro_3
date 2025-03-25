using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackActionHitbox : AttackData
{
    public bool Debug;
    [field: SerializeField] public Rect Hitbox { get; private set; }
}
