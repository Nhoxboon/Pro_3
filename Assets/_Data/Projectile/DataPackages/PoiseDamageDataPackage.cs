using System;
using UnityEngine;


    [Serializable]
    public class PoiseDamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
