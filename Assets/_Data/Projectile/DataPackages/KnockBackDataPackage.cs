using System;
using UnityEngine;


    [Serializable]
    public class KnockBackDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Strength;
        [field: SerializeField] public Vector2 Angle;
    }