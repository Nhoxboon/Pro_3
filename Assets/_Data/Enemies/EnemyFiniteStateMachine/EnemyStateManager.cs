using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class EnemyStateManager : NhoxBehaviour
{
    protected FiniteStateMachine stateMachine;

    [Header("Enemy State Manager")]
    [SerializeField] protected Vector2 workSpace;

    [SerializeField] protected EnemyDataSO enemyDataSO;

    [SerializeField] protected Transform detectedZone;

    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl => enemyCtrl;

    [SerializeField] protected Core core;
    public Core Core => core;
    

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
        LoadCore();
    }

    protected void LoadDetectedZone()
    {
        if (detectedZone != null) return;
        detectedZone = transform.Find("DetectedZone");
        Debug.Log(transform.name + " LoadDetectedZone", gameObject);
    }

    protected void LoadEnemyDataSO()
    {
        if (enemyDataSO != null) return;
        enemyDataSO = Resources.Load<EnemyDataSO>("Enemies/" + transform.name + "/" + transform.name);
        Debug.Log(transform.name + " LoadEnemyDataSO", gameObject);
    }

    protected void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        enemyCtrl = GetComponent<EnemyCtrl>();
        Debug.Log(transform.name + " LoadEnemyCtrl", gameObject);
    }

    protected void LoadCore()
    {
        if (core != null) return;
        core = transform.GetComponentInChildren<Core>();
        Debug.Log(transform.name + " LoadCore", gameObject);
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
        return Physics2D.Raycast(detectedZone.position, transform.right, enemyDataSO.minAgroDistance, enemyDataSO.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(detectedZone.position, transform.right, enemyDataSO.maxAgroDistance, enemyDataSO.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(detectedZone.position, transform.right, enemyDataSO.closeRangeActionDistance, enemyDataSO.whatIsPlayer);
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
