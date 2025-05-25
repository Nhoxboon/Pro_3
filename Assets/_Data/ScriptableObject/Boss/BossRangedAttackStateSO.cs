using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BossRangedAttackStateSO", menuName = "ScriptableObject/Boss Data/State Data/Ranged Attack State")]
public class BossRangedAttackStateSO : EnemyRangedAttackStateSO
{
    public float minAttackCooldown = 10f;
    public float maxAttackCooldown = 30f;
}