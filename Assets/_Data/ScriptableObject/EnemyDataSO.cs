using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObject/Enemy Data/Base Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Idle State")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;

    [Header("Move State")]
    public float movementSpeed = 3f;

    [Header("Detected Player State")]
    public float longRangeActionTime = 1.5f;
    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;
    public LayerMask whatIsPlayer;

    [Header("Charge State")]
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;
}
