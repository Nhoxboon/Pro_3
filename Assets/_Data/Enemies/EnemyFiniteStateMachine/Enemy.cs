using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : NhoxBehaviour
{
    protected FiniteStateMachine stateMachine;
    public FiniteStateMachine StateMachine => stateMachine;

    [SerializeField] protected Vector2 workSpace;

    [SerializeField] protected EnemyDataSO enemyDataSO;
    public EnemyDataSO EnemyDataSO => enemyDataSO;

    [SerializeField] protected Transform detectedZone;

    [SerializeField] protected EnemyGetAnimationEvent getAnimEvent;
    public EnemyGetAnimationEvent GetAnimEvent => getAnimEvent;

    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl => enemyCtrl;

    [SerializeField] protected Core core;
    public Core Core => core;

    [Header("Status")]
    [SerializeField] protected Stats stats;

    protected bool isStunned;
    protected float currentStunResistance;
    protected float lastDamageTime;
    
    protected override void Awake()
    {
        base.Awake();
        
        core.ParryReceiver.OnParried += HandleParry;
        currentStunResistance = enemyDataSO.stunResistance;

        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        core.LogicUpdate();

        stateMachine.CurrentState.LogicUpdate();

        enemyCtrl.EnemyAnimation.YVelocityAnimation(core.Movement.Rb.velocity.y);

        if (Time.time >= lastDamageTime + enemyDataSO.stunRecoveryTime) {
            ResetStunResistance();
        }
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDetectedZone();
        LoadEnemyCtrl();
        LoadEnemyDataSO();
        LoadAnimationEvent();
        LoadCore();
        LoadStats();
    }

    protected void LoadDetectedZone()
    {
        if (this.detectedZone != null) return;
        this.detectedZone = transform.parent.Find("DetectedZone");
        Debug.Log(transform.name + " LoadDetectedZone", gameObject);
    }

    protected void LoadEnemyDataSO()
    {
        if (enemyDataSO != null) return;
        enemyDataSO = Resources.Load<EnemyDataSO>("Enemies/" + transform.parent.name);
        Debug.Log(transform.name + " LoadEnemyDataSO", gameObject);
    }

    protected void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.Log(transform.name + " LoadEnemyCtrl", gameObject);
    }

    protected void LoadCore()
    {
        if (core != null) return;
        core = transform.parent.GetComponentInChildren<Core>();
        Debug.Log(transform.name + " LoadCore", gameObject);
    }

    protected void LoadStats()
    {
        if (stats != null) return;
        stats = transform.parent.GetComponentInChildren<Stats>();
        Debug.Log(transform.name + " LoadStats", gameObject);
    }

    protected void LoadAnimationEvent()
    {
        if (getAnimEvent != null) return;
        getAnimEvent = GetComponent<EnemyGetAnimationEvent>();
        Debug.Log(transform.name + " LoadAnimationEvent", gameObject);
    }
    
    protected virtual void HandleParry()
    {
		
    }
    
    public virtual void ResetStunResistance() {
        isStunned = false;
        currentStunResistance = enemyDataSO.stunResistance;
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(detectedZone.position, transform.parent.right, enemyDataSO.minAgroDistance, enemyDataSO.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(detectedZone.position, transform.parent.right, enemyDataSO.maxAgroDistance, enemyDataSO.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(detectedZone.position, transform.parent.right, enemyDataSO.closeRangeActionDistance, enemyDataSO.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        workSpace.Set(core.Movement.Rb.velocity.x, velocity);
        core.Movement.Rb.velocity = workSpace;
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.maxAgroDistance), 0.2f);

        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.closeRangeActionDistance), 0.2f);

    }
}
