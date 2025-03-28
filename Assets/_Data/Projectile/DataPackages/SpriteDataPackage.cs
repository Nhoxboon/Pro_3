using System;
using UnityEngine;


    [Serializable]
    public class SpriteDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }