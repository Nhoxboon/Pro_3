using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : NhoxBehaviour
{
    [SerializeField] protected Animator baseAnimator;
    [SerializeField] protected Animator weaponAnimator;

    [SerializeField] protected Core core;

    [SerializeField] protected PlayerAttackState state;

    [SerializeField] protected int attackCounter;

    [SerializeField] protected WeaponDataSO weaponDataSO;

    protected override void Awake()
    {
        base.Awake();
        HideWeapon();
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBaseAnimator();
        LoadWeaponAnimator();
        LoadWeaponDataSO();
    }

    protected virtual void LoadBaseAnimator()
    {
        if (baseAnimator != null) return;
        baseAnimator = transform.Find("Base").GetComponentInChildren<Animator>();
        Debug.Log(transform.name + " :LoadBaseAnimator", gameObject);
    }

    protected virtual void LoadWeaponAnimator()
    {
        if (weaponAnimator != null) return;
        weaponAnimator = transform.Find("Weapon").GetComponentInChildren<Animator>();
        Debug.Log(transform.name + " :LoadWeaponAnimator", gameObject);
    }

    protected virtual void LoadWeaponDataSO()
    {
        if (weaponDataSO != null) return;
        weaponDataSO = Resources.Load<WeaponDataSO>("Weapons/" + transform.name);
        Debug.Log(transform.name + " :LoadWeaponDataSO", gameObject);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponDataSO.amountOfAttacks)
        {
            attackCounter = 0;
        }

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    protected virtual void HideWeapon()
    {
        gameObject.SetActive(false);
    }

    #region Animation Triggers Events
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponDataSO.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOffFlip()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlip()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {
        //For override
    }
    #endregion

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }


}
