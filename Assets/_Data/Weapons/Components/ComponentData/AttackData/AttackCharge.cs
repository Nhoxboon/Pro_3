using System;
using UnityEngine;

[Serializable]
public class AttackCharge : AttackData
{
    public float chargeTime;
    [Range(0, 1)] public int initialChargeAmount;
    public int numberOfCharges;

    public string chargeIncreaseIndicatorParticalName;

    public string fullyChargedIndicatorParticleName;

    public Vector2 particlesOffset;
}