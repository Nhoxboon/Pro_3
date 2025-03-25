using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : WeaponComponent<AttackMovementData, AttackMovement>
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventHandler.OnStartMovement += HandleStartMovement;
        EventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventHandler.OnStartMovement -= HandleStartMovement;
        EventHandler.OnStopMovement -= HandleStopMovement;
    }

    protected void HandleStartMovement()
    {
        coreMovement.SetVelocity(currentAttackData.velocity, currentAttackData.direction, coreMovement.FacingDirection);
    }

    protected void HandleStopMovement()
    {
        coreMovement.SetVelocityZero();
    }
}
