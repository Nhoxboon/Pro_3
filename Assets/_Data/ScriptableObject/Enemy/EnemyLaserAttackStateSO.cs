using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLaserAttackStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Laser Attack State")]
public class EnemyLaserAttackStateSO : ScriptableObject
{
    public float chargeTime;
    public float laserDuration = 2f;
    [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
}
