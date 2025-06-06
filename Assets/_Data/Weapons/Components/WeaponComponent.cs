using UnityEngine;

public abstract class WeaponComponent : NhoxBehaviour
{
    [SerializeField] protected Weapon weapon;

    protected bool isAttacking;

    protected WeaponGetAnimationEvent EventHandler => weapon.GetAnimationEvent;
    protected Core Core => weapon.Core;

    protected float attackStartTime => weapon.AttackStartTime;

    protected override void Start()
    {
        base.Start();
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void OnDestroy()
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
        if (weapon != null) return;
        weapon = GetComponent<Weapon>();
        //Debug.Log(transform.name + " LoadWeapon", gameObject);
    }

    public virtual void Init()
    {
        //For override
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

public abstract class WeaponComponent<T1, T2> : WeaponComponent
    where T1 : ComponentDataAbstract<T2> where T2 : AttackData
{
    protected T2 currentAttackData;
    protected T1 data;
    public T2 CurrentAttackData => currentAttackData;

    protected override void Awake()
    {
        base.Awake();
        GetData();
    }

    public override void Init()
    {
        base.Init();
        GetData();
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentAttackData = data.GetAttackData(weapon.CurrentAttack);
    }

    protected void GetData()
    {
        data = weapon.WeaponDataSO.GetData<T1>();
    }
}