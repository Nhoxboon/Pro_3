using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : WeaponComponent<AttackMovementData, AttackMovement>
{
    protected float velocity;
    protected Vector2 direction;

    private void HandleStartMovement()
    {
        velocity = currentAttackData.velocity;
        direction = currentAttackData.direction;
            
        SetVelocity();
    }

    private void HandleStopMovement()
    {
        velocity = 0f;
        direction = Vector2.zero;

        SetVelocity();
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();
            
        velocity = 0f;
        direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(!isAttacking) return;
            
        SetVelocityX();
    }

    private void SetVelocity()
    {
        Core.Movement.SetVelocity(velocity, direction, Core.Movement.FacingDirection);
    }

    private void SetVelocityX()
    {
        Core.Movement.SetVelocityX((direction * velocity).x * Core.Movement.FacingDirection);
    }

    protected override void Start()
    {
        base.Start();

        EventHandler.OnStartMovement += HandleStartMovement;
        EventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
            
        EventHandler.OnStartMovement -= HandleStartMovement;
        EventHandler.OnStopMovement -= HandleStopMovement;
    }
}
