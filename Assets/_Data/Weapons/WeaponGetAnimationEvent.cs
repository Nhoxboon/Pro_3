using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGetAnimationEvent : NhoxBehaviour
{
    [SerializeField] protected Weapon weapon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeapon();
    }

    protected void LoadWeapon()
    {
        if (weapon != null) return;
        weapon = GetComponentInParent<Weapon>();
        Debug.Log(transform.name + " LoadWeapon: ", gameObject);
    }

    protected void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    protected void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }

    protected void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    protected void AnimationTurnOffFlip()
    {
        weapon.AnimationTurnOffFlip();
    }

    protected void AnimationTurnOnFlip()
    {
        weapon.AnimationTurnOnFlip();
    }

    protected void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}
