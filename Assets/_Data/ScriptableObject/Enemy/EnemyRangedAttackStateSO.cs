using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRangedAttackStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Ranged Attack State")]
public class EnemyRangedAttackStateSO : ScriptableObject
{
    [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
}
