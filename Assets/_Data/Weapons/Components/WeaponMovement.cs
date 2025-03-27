using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : WeaponComponent<AttackMovementData, AttackMovement>
{

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

    protected void HandleStartMovement()
    {
        Core.Movement.SetVelocity(currentAttackData.velocity, currentAttackData.direction, Core.Movement.FacingDirection);
    }

    protected void HandleStopMovement()
    {
        Core.Movement.SetVelocityZero();
    }
}
