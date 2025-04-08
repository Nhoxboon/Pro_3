using System;
using UnityEngine;

public class Weapon : NhoxBehaviour
{
    [SerializeField] protected WeaponDataSO weaponDataSO;

    [SerializeField] protected float attackResetCooldown = 3;

    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject baseGameObj;

    [SerializeField] protected GameObject weaponSpriteGameObj;

    [SerializeField] protected WeaponGetAnimationEvent getAnimationEvent;

    [SerializeField] protected float attackStartTime;

    [SerializeField] protected int currentAttack;

    [SerializeField] protected bool currentInput;

    [SerializeField] protected Core core;

    protected TimeNotifier attackCounterResetTimeNotifier;
    public WeaponDataSO WeaponDataSO => weaponDataSO;
    public Animator Anim => anim;
    public GameObject BaseGameObj => baseGameObj;
    public GameObject WeaponSpriteGameObj => weaponSpriteGameObj;
    public WeaponGetAnimationEvent GetAnimationEvent => getAnimationEvent;
    public float AttackStartTime => attackStartTime;
    public int CurrentAttack => currentAttack;

    public bool CurrentInput
    {
        get => currentInput;
        set
        {
            if (currentInput != value)
            {
                currentInput = value;
                OnCurrentInputChange?.Invoke(currentInput);
            }
        }
    }

    public Core Core => core;

    protected override void Awake()
    {
        base.Awake();
        attackCounterResetTimeNotifier = new TimeNotifier();
    }

    private void Update()
    {
        attackCounterResetTimeNotifier.Tick();
    }

    private void OnEnable()
    {
        getAnimationEvent.OnUseInput += HandleUseInput;
        attackCounterResetTimeNotifier.OnNotify += ResetAttack;
    }

    private void OnDisable()
    {
        getAnimationEvent.OnUseInput -= HandleUseInput;
        attackCounterResetTimeNotifier.OnNotify -= ResetAttack;
    }

    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnUseInput;

    public event Action<bool> OnCurrentInputChange;

    public void SetData(WeaponDataSO data)
    {
        weaponDataSO = data;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBaseGameObj();
        LoadWeaponSpriteGameObj();
        LoadAnim();
        LoadWpGetAnimationEvent();
        LoadCore();
    }

    protected void LoadBaseGameObj()
    {
        if (baseGameObj != null) return;
        baseGameObj = transform.Find("Base").gameObject;
        Debug.Log(transform.name + " LoadBaseGameObj", gameObject);
    }

    protected void LoadWeaponSpriteGameObj()
    {
        if (weaponSpriteGameObj != null) return;
        weaponSpriteGameObj = transform.Find("WeaponSprite").gameObject;
        Debug.Log(transform.name + " LoadWeaponSpriteGameObj", gameObject);
    }

    protected void LoadAnim()
    {
        if (anim != null) return;
        anim = baseGameObj.GetComponent<Animator>();
        Debug.Log(transform.name + " LoadAnim", gameObject);
    }

    protected void LoadWpGetAnimationEvent()
    {
        if (getAnimationEvent != null) return;
        getAnimationEvent = baseGameObj.GetComponent<WeaponGetAnimationEvent>();
        Debug.Log(transform.name + " LoadWpGetAnimationEvent", gameObject);
    }

    protected void LoadCore()
    {
        if (core != null) return;
        core = transform.parent.parent.GetComponentInChildren<Core>();
        Debug.Log(transform.name + " LoadCore", gameObject);
    }

    public void Enter()
    {
        if (currentAttack >= weaponDataSO.numberOfAttacks) currentAttack = 0;

        attackStartTime = Time.time;

        attackCounterResetTimeNotifier.Disable();

        anim.SetBool("attack", true);
        anim.SetInteger("counter", currentAttack);

        OnEnter?.Invoke();
    }

    public void Exit()
    {
        anim.SetBool("attack", false);

        currentAttack++;
        attackCounterResetTimeNotifier.Init(attackResetCooldown);

        OnExit?.Invoke();
    }

    protected void ResetAttack()
    {
        currentAttack = 0;
    }

    protected void HandleUseInput()
    {
        OnUseInput?.Invoke();
    }
}