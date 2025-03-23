using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : NhoxBehaviour
{
    [SerializeField] protected Weapon weapon;

    protected bool isAttacking;

    protected virtual void OnEnable()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void OnDisable()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeapon();
    }

    protected void LoadWeapon()
    {
        if (this.weapon != null) return;
        this.weapon = GetComponent<Weapon>();
        Debug.Log(transform.name + " LoadWeapon", gameObject);
    }

    protected virtual void HandleEnter()
    {
        isAttacking = true;
    }

    protected virtual void HandleExit()
    {
        isAttacking = false;
    }
}
