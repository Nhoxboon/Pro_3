using System.Collections;
using UnityEngine;

public abstract class EnemyStateManager : NhoxBehaviour
{
    [Header("Enemy State Manager")] [SerializeField]
    protected Vector2 workSpace;

    [SerializeField] protected EnemyDataSO enemyDataSO;
    public EnemyDataSO EnemyDataSO => enemyDataSO;
    [SerializeField] protected EnemyAudioDataSO audioDataSO;

    [SerializeField] protected Transform detectedZone;

    [SerializeField] protected EnemyCtrl enemyCtrl;

    [SerializeField] protected Core core;
    
    [SerializeField] protected Transform meleeAttackPosition;

    protected float currentStunResistance;

    protected bool isStunned;
    protected float lastDamageTime;
    protected FiniteStateMachine stateMachine;
    public EnemyAudioDataSO AudioDataSO => audioDataSO;
    public EnemyCtrl EnemyCtrl => enemyCtrl;
    public Core Core => core;
    
    public int currentPointIndex;

    protected override void Awake()
    {
        base.Awake();

        core.ParryReceiver.OnParried += HandleParry;
        currentStunResistance = enemyDataSO.stunResistance;

        core.Stats.Health.OnCurrentValueZero += HandleDeath;
        core.Stats.Poise.OnCurrentValueZero += HandlePoiseZero;
        core.Stats.Health.OnValueDecreased += HandleHealthDecrease;

        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        core.LogicUpdate();

        stateMachine.CurrentState.LogicUpdate();

        enemyCtrl.EnemyAnimation.YVelocityAnimation(core.Movement.Rb.velocity.y);

        if (Time.time >= lastDamageTime + enemyDataSO.stunRecoveryTime) ResetStunResistance();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    protected virtual void OnDestroy()
    {
        core.ParryReceiver.OnParried -= HandleParry;
        core.Stats.Health.OnCurrentValueZero -= HandleDeath;
        core.Stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
        core.Stats.Health.OnValueDecreased -= HandleHealthDecrease;
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.maxAgroDistance), 0.2f);

        Gizmos.DrawWireSphere(detectedZone.position + (Vector3)(Vector2.right * enemyDataSO.closeRangeActionDistance),
            0.2f);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyAudioDataSO();
        LoadDetectedZone();
        LoadEnemyCtrl();
        LoadEnemyDataSO();
        LoadCore();
        LoadMeleeAttackPosition();
    }

    protected void LoadDetectedZone()
    {
        if (detectedZone != null) return;
        detectedZone = transform.Find("DetectedZone");
        Debug.Log(transform.name + " LoadDetectedZone", gameObject);
    }

    protected virtual void LoadEnemyDataSO()
    {
        if (enemyDataSO != null) return;
        enemyDataSO = Resources.Load<EnemyDataSO>("Enemies/" + transform.name + "/" + transform.name);
        Debug.Log(transform.name + " LoadEnemyDataSO", gameObject);
    }

    protected virtual void LoadEnemyAudioDataSO()
    {
        if (audioDataSO != null) return;
        audioDataSO = Resources.Load<EnemyAudioDataSO>("Enemies/" + transform.name + "/" + transform.name + "Audio");
        Debug.Log(transform.name + " LoadEnemyAudioDataSO", gameObject);
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
    
    protected void LoadMeleeAttackPosition()
    {
        if (meleeAttackPosition != null) return;
        meleeAttackPosition = transform.Find("Attack/MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackPosition", gameObject);
    }

    protected virtual void HandleParry()
    {

    }
    
    protected abstract void HandleDeath();

    protected virtual void HandlePoiseZero()
    {

    }

    protected abstract void HandleHealthDecrease();

    protected void Flash()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        enemyCtrl.Sr.material.SetInt("_Flash", 1);
        yield return new WaitForSeconds(0.3f);
        enemyCtrl.Sr.material.SetInt("_Flash", 0);
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = enemyDataSO.stunResistance;
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(detectedZone.position, transform.right, enemyDataSO.minAgroDistance,
            enemyDataSO.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(detectedZone.position, transform.right, enemyDataSO.maxAgroDistance,
            enemyDataSO.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(detectedZone.position, transform.right, enemyDataSO.closeRangeActionDistance,
            enemyDataSO.whatIsPlayer);
    }
    
    public virtual Vector3 CheckPlayerPosition()
    {
        return PlayerCtrl.Instance.transform.position;
    }
}