using System;
using UnityEngine;

[Serializable]
public class AttackChargeToProjectileSpawner : AttackData
{
    [Range(0f, 360f)] public float angleVariation;
}