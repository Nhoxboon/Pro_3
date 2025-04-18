using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDodgeStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Dodge State")]
public class EnemyDodgeStateSO : ScriptableObject
{
    public float dodgeSpeed = 15f;
    public float dodgeTime = 0.2f;
    public float dodgeCooldown = 4f;
    public Vector2 dodgeAngle = new Vector2(2f, 1f);
}
