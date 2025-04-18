using UnityEngine;

[CreateAssetMenu(fileName = "EnemyChargeStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Charge State")]
public class EnemyChargeStateSO : ScriptableObject
{
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;
}
