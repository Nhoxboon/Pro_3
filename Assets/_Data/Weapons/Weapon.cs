using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : NhoxBehaviour
{
    [SerializeField] protected WeaponDataSO weaponDataSO;
    public WeaponDataSO WeaponDataSO => weaponDataSO;

    [SerializeField] protected float attackResetCooldown = 3;

    public event Action OnEnter;
    public event Action OnExit;

    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject baseGameObj;
    public GameObject BaseGameObj => baseGameObj;

    [SerializeField] protected GameObject weaponSpriteGameObj;
    public GameObject WeaponSpriteGameObj => weaponSpriteGameObj;

    [SerializeField] protected WeaponGetAnimationEvent getAnimationEvent;

    [SerializeField] protected int currentAttack;
    public int CurrentAttack => currentAttack;

    protected Timer attackResetTimer;

    protected override void Awake()
    {
        base.Awake();
        attackResetTimer = new Timer(attackResetCooldown);
    }

    private void Update()
    {
        attackResetTimer.Tick();
    }

    private void OnEnable()
    {
        getAnimationEvent.OnFinish += Exit;
        attackResetTimer.OnTimerEnd += ResetAttack;
    }

    private void OnDisable()
    {
        getAnimationEvent.OnFinish -= Exit;
        attackResetTimer.OnTimerEnd -= ResetAttack;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBaseGameObj();
        LoadWeaponSpriteGameObj();
        LoadAnim();
        LoadWpGetAnimationEvent();
    }

    protected void LoadBaseGameObj()
    {
        if (this.baseGameObj != null) return;
        this.baseGameObj = transform.Find("Base").gameObject;
        Debug.Log(transform.name + " LoadBaseGameObj", gameObject);
    }

    protected void LoadWeaponSpriteGameObj()
    {
        if (this.weaponSpriteGameObj != null) return;
        this.weaponSpriteGameObj = transform.Find("WeaponSprite").gameObject;
        Debug.Log(transform.name + " LoadWeaponSpriteGameObj", gameObject);
    }

    protected void LoadAnim()
    {
        if (this.anim != null) return;
        this.anim = baseGameObj.GetComponent<Animator>();
        Debug.Log(transform.name + " LoadAnim", gameObject);
    }

    protected void LoadWpGetAnimationEvent()
    {
        if(this.getAnimationEvent != null) return;
        this.getAnimationEvent = baseGameObj.GetComponent<WeaponGetAnimationEvent>();
        Debug.Log(transform.name + " LoadWpGetAnimationEvent", gameObject);
    }

    protected void LoadWeaponDataSO()
    {
        if (this.weaponDataSO != null) return;
        this.weaponDataSO = Resources.Load<WeaponDataSO>("Weapon");
        Debug.Log(transform.name + " LoadWeaponDataSO", gameObject);
    }

    public void Enter()
    {
        if (currentAttack >= weaponDataSO.numberOfAttacks)
        {
            currentAttack = 0;
        }

        attackResetTimer.StopTimer();

        anim.SetBool("attack", true);
        anim.SetInteger("counter", currentAttack);

        OnEnter?.Invoke();
    }

    protected void Exit()
    {
        anim.SetBool("attack", false);

        currentAttack++;
        attackResetTimer.StartTimer();

        OnExit?.Invoke();
    }

    protected void ResetAttack() => currentAttack = 0;
}
