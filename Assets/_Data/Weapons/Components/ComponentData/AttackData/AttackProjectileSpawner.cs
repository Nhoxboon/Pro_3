using System;
using UnityEngine;

[Serializable]
public class AttackProjectileSpawner : AttackData
{
    public bool isChargeable;
    
    public AudioClip spawnSound;
    [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
}