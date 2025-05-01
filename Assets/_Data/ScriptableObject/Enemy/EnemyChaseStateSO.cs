using UnityEngine;

[CreateAssetMenu(fileName = "EnemyChaseStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Chase State")]
public class EnemyChaseStateSO : ScriptableObject
{
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;
}
