
using UnityEngine;

[CreateAssetMenu(fileName = "BossDataSO", menuName = "ScriptableObject/Boss Data/Base Data")]
public class BossDataSO : EnemyDataSO
{
    [Header("Boss Data")]
    public float phaseChangeTime = 1f;
}
