using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EntityStatsDataSO", menuName = "ScriptableObject/Enity Data/Stats Data")]
public class EntityStatsDataSO : ScriptableObject
{
    public float health = 100;
    public float stagger = 50;
    public float poiseRecoveryRate = 3;
}
