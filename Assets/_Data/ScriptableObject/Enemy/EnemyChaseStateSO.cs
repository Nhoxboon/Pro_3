using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyChaseStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Chase State")]
public class EnemyChaseStateSO : ScriptableObject
{
    [FormerlySerializedAs("chargeSpeed")] public float chaseSpeed = 6f;
    public float chargeTime = 2f;
}
