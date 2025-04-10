using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObject/Enemy Data/Base Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("General")]
    public float damageHopSpeed = 10f;

    [Header("Idle State")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;

    [Header("Move State")]
    public float movementSpeed = 3f;

    [Header("Detected Player State")]
    public float longRangeActionTime = 3f;
    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;
    public LayerMask whatIsPlayer;

    [Header("Attack State")]
    public float closeRangeActionDistance = 1f;
    public float attackRadius = 0.75f;
    public float attackDamage = 10f;
    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 10f;

    [Header("Ranged Attack State")]
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 5f;

    [Header("Charge State")]
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;

    [Header("Look For Player State")]
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f;

    [Header("Stun State")]
    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    public float stunTime = 3f;
    public float stunKnockBackTime = 0.2f;

    [Header("Dodge State")]
    public float dodgeSpeed = 15f;
    public float dodgeTime = 0.2f;
    public float dodgeCooldown = 2f;
    public Vector2 dodgeAngle;
}
