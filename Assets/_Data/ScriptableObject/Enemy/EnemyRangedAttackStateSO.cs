using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRangedAttackStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Ranged Attack State")]

public class EnemyRangedAttackStateSO : ScriptableObject
{
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 8f;
}
