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
}
