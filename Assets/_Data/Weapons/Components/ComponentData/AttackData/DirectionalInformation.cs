using System;
using UnityEngine;

[Serializable]
public class DirectionalInformation
{
    [Range(-180f, 180f)] public float minAngle;
    [Range(-180f, 180f)] public float maxAngle;
    [Range(0f, 1f)] public float damageAbsorption;
    [Range(0f, 1f)] public float knockbackAbsorption;
    [Range(0f, 1f)] public float poiseAbsorption;

    public bool IsAngleBetween(float angle)
    {
        if (maxAngle > minAngle) return angle >= minAngle && angle <= maxAngle;

        return (angle >= minAngle && angle <= 180f) || (angle <= maxAngle && angle >= -180f);
    }
}