using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : NhoxBehaviour
{
    protected FiniteStateMachine stateMachine;
    public FiniteStateMachine StateMachine => stateMachine;

    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    [SerializeField] protected int facingDirection;
    public int FacingDirection => facingDirection;

    [SerializeField] protected Vector2 workSpace;

    [SerializeField] protected EnemyDataSO enemyDataSO;

    [SerializeField] protected Transform detectedZone;

    [SerializeField] protected EnemyGetAnimationEvent getAnimEvent;
    public EnemyGetAnimationEvent GetAnimEvent => getAnimEvent;

    [Header("DamageReceiver")]
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float currentStunResistance;
    [SerializeField] protected float lastDamageTime;
    [SerializeField] protected int lastDamageDirection;
    public int LastDamageDirection;
    [SerializeField] protected bool isStunned;

    protected override void Start()
    {
        base.Start();
        facingDirection = 1;
        currentHealth = enemyDataSO.maxHealth;
        currentStunResistance = enemyDataSO.stunResistance;

        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        if(Time.time >= lastDamageTime + enemyDataSO.stunRecoveryTime)
        {
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
        LoadRigidbody2D();
        LoadDetectedZone();
        LoadEnemyDataSO();
        LoadAnimationEvent();
    }

    protected void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = this.GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2D", gameObject);
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
        enemyDataSO = Resources.Load<EnemyDataSO>("Enemies/Pig");
        Debug.Log(transform.name + " LoadEnemyDataSO", gameObject);
    }

    protected void LoadAnimationEvent()
    {
        if (getAnimEvent != null) return;
        getAnimEvent = GetComponent<EnemyGetAnimationEvent>();
        Debug.Log(transform.name + " LoadAnimationEvent", gameObject);
    }

    public virtual void SetVelocityX(float velocity)
    {
        workSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = workSpace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = workSpace;
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
        workSpace.Set(rb.velocity.x, velocity);
        rb.velocity = workSpace;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(enemyDataSO.damageHopSpeed);
        if(attackDetails.position.x > transform.parent.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0)
        {
            isStunned = true;
        }
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = enemyDataSO.stunResistance;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.parent.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.maxAgroDistance), 0.2f);

        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.closeRangeActionDistance), 0.2f);

    }
}
