using System;
using UnityEngine;

[Serializable]
public struct ProjectileSpawnInfo
{
    // Offset from the players transform
    [field: SerializeField] public Vector2 Offset { get; private set; }

    // Direction that the projectile spawns in, relative to the facing direction of the player
    [field: SerializeField] public Vector2 Direction { get; private set; }

    [field: SerializeField] public string ProjectilePrefabName { get; private set; }

    [field: SerializeField] public DamageDataPackage DamageData { get; private set; }
    [field: SerializeField] public KnockBackDataPackage KnockBackData { get; private set; }
    [field: SerializeField] public PoiseDamageDataPackage PoiseDamageData { get; private set; }
}