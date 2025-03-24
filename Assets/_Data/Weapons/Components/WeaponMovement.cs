using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : WeaponComponent<AttackMovementData, AttackMovement>
{
    [SerializeField] protected Movement coreMovement;

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

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCoreMovement();
    }

    protected void LoadCoreMovement()
    {
        if (coreMovement != null) return;
        coreMovement = Core.GetComponentInChildren<Movement>();
        Debug.Log(transform.name + " LoadCoreMovement", gameObject);
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
