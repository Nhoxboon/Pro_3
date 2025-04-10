using System;
using UnityEngine;

[Serializable]
public class AttackProjectileSpawner : AttackData
{
    // This is an array as each attack can spawn multiple projectiles.
    public bool isChargeable;
    [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
}