using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMeleeAttackStateSO", menuName = "ScriptableObject/Enemy Data/State Data/Melee Attack State")]
public class EnemyMeleeAttackStateSO : ScriptableObject
{
    public float attackRadius = 0.75f;
    public float attackDamage = 10f;

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 10f;

    public float poiseDamage = 30f;

    public LayerMask whatIsPlayer;
}