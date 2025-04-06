
using System;
using UnityEngine;

[Serializable]
public class AttackTargeter : AttackData
{
    public Rect area;
    public LayerMask damageableLayer;
}
