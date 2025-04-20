using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "ScriptableObject/Player Data/Base Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
    
    [Header("Stun State")]
    public float stunTime = 2f;

    [Header("Crouch State")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 1.66f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset = new Vector2(0.45f, 0.9f);
    public Vector2 stopOffset = new Vector2(0.4f, 0.75f);

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distanceBetweenImages = 0.5f;
}
