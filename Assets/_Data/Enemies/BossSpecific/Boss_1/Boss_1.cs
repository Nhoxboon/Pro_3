
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : EnemyStateManager
{
    #region State Variables

    protected Boss_1SleepState sleepState;

    protected Boss_1IdleState idleState;
    public Boss_1IdleState IdleState => idleState;

    protected Boss_1MoveState moveState;
    public Boss_1MoveState MoveState => moveState;

    protected Boss_1MeleeAttackState meleeAttackState;
    public Boss_1MeleeAttackState MeleeAttackState => meleeAttackState;

    protected Boss_1RangedAttackState rangedAttackState;
    public Boss_1RangedAttackState RangedAttackState => rangedAttackState;

    protected Boss_1LaserAttackState laserAttackState;
    public Boss_1LaserAttackState LaserAttackState => laserAttackState;

    protected Boss_1MoveByPointState moveByPointState;
    public Boss_1MoveByPointState MoveByPointState => moveByPointState;

    protected Boss_1PhaseChangeState phaseChangeState;

    protected Boss_1DeadState deadState;
    #endregion

    [Header("Boss 1")]
    [SerializeField] protected Transform rangedAttackPosition;
    [SerializeField] protected Transform laserAttackPosition;

    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected BossRangedAttackStateSO rangedAttackDataSO;
    public BossRangedAttackStateSO RangedAttackStateSO => rangedAttackDataSO;
    [SerializeField] protected EnemyLaserAttackStateSO laserAttackDataSO;

    [Header("Move Points")]
    [SerializeField] protected Transform pointsHolder;
    [SerializeField] protected List<Transform> movePoints;
    public List<Transform> MovePoints => movePoints;

    public float lastRangedAttackTime;

    [SerializeField] protected SpriteRenderer chargeSprite;
    public SpriteRenderer ChargeSprite => chargeSprite;

    protected bool isPhaseChange = false;
    public bool IsPhaseChange => isPhaseChange;

    [SerializeField] protected LaserWarningMovement laserWarning;
    public LaserWarningMovement LaserWarning => laserWarning;

    [Header("Boss Room")]
    [SerializeField] protected bool isPlayerInRoom = false;
    [SerializeField] protected BossRoomTrigger bossRoomTrigger;
    
    [SerializeField] protected BossHealthBarUI bossHealthBarUI;
    public BossHealthBarUI BossHealthBarUI => bossHealthBarUI;

    protected override void Awake()
    {
        base.Awake();

        sleepState = new Boss_1SleepState(this, stateMachine, "sleep", enemyDataSO, audioDataSO, this);
        idleState = new Boss_1IdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        moveState = new Boss_1MoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        meleeAttackState = new Boss_1MeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO, meleeAttackPosition, meleeAttackDataSO, this);
        rangedAttackState = new Boss_1RangedAttackState(this, stateMachine, "rangedAttack", enemyDataSO, audioDataSO, rangedAttackPosition, rangedAttackDataSO, this);
        laserAttackState = new Boss_1LaserAttackState(this, stateMachine, "laserAttack", enemyDataSO, audioDataSO, laserAttackPosition, laserAttackDataSO, this);
        moveByPointState = new Boss_1MoveByPointState(this, stateMachine, "move", enemyDataSO, audioDataSO, movePoints, this);
        phaseChangeState = new Boss_1PhaseChangeState(this, stateMachine, "phaseChange", enemyDataSO, audioDataSO, this);
        deadState = new Boss_1DeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);

        bossRoomTrigger.OnPlayerEnter += StartBossFight;
    }

    protected override void Start()
    {
        base.Start();

        chargeSprite.enabled = false;
        stateMachine.Initialize(sleepState);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        bossRoomTrigger.OnPlayerEnter -= StartBossFight;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRangedAttackPosition();
        LoadLaserAttackPosition();
        LoadMeleeAttackDataSO();
        LoadRangedAttackDataSO();
        LoadLaserAttackDataSO();
        LoadPoints();
        LoadChargeSprite();
        LoadLaserWarning();
        LoadBossRoomTrigger();
        LoadBossHealthBarUI();
    }

    protected override void LoadEnemyDataSO()
    {
        if (enemyDataSO != null) return;
        enemyDataSO = Resources.Load<EnemyDataSO>("Boss/Boss_1");
        Debug.Log(transform.name + " LoadEnemyDataSO", gameObject);
    }

    protected override void LoadEnemyAudioDataSO()
    {
        if(audioDataSO != null) return;
        audioDataSO = Resources.Load<EnemyAudioDataSO>("Boss/Boss_1Audio");
        Debug.Log(transform.name + " LoadEnemyAudioDataSO", gameObject);
    }

    protected void LoadMeleeAttackDataSO()
    {
        if (meleeAttackDataSO != null) return;
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Boss/Boss_1MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }

    protected void LoadRangedAttackDataSO()
    {
        if (rangedAttackDataSO != null) return;
        rangedAttackDataSO = Resources.Load<BossRangedAttackStateSO>("Boss/Boss_1RangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackDataSO", gameObject);
    }

    protected void LoadLaserAttackDataSO()
    {
        if (laserAttackDataSO != null) return;
        laserAttackDataSO = Resources.Load<EnemyLaserAttackStateSO>("Boss/Boss_1LaserAttack");
        Debug.Log(transform.name + " LoadLaserAttackDataSO", gameObject);
    }

    protected void LoadRangedAttackPosition()
    {
        if (rangedAttackPosition != null) return;
        rangedAttackPosition = transform.Find("Attack/RangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackPosition", gameObject);
    }

    protected void LoadLaserAttackPosition()
    {
        if (laserAttackPosition != null) return;
        laserAttackPosition = transform.Find("Attack/LaserAttack");
        Debug.Log(transform.name + " LoadLaserAttackPosition", gameObject);
    }

    protected virtual void LoadPoints()
    {
        if (pointsHolder != null) return;
        pointsHolder = GameObject.Find("BossMovePoints").transform;
        foreach (Transform point in pointsHolder)
        {
            movePoints.Add(point);
        }
        Debug.Log(transform.name + " LoadPoints", gameObject);
    }

    protected void LoadChargeSprite()
    {
        if (chargeSprite != null) return;
        chargeSprite = laserAttackPosition.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + " LoadChargeSprite", gameObject);
    }

    protected void LoadLaserWarning()
    {
        if (laserWarning != null) return;
        laserWarning = GetComponentInChildren<LaserWarningMovement>();
        Debug.Log(transform.name + " LoadLaserWarning", gameObject);
    }

    protected void LoadBossRoomTrigger()
    {
        if(bossRoomTrigger != null) return;
        bossRoomTrigger = transform.parent.GetComponentInChildren<BossRoomTrigger>();
        Debug.Log(transform.name + " LoadBossRoomTrigger", gameObject);
    }
    
    protected void LoadBossHealthBarUI()
    {
        if (bossHealthBarUI != null) return;
        bossHealthBarUI = GameObject.FindGameObjectWithTag("BossHealthBarUI").GetComponent<BossHealthBarUI>();
        Debug.Log(transform.name + " LoadBossHealthBarUI", gameObject);
    }
    #endregion

    protected void StartBossFight()
    {
        bossHealthBarUI.SetCore(core);
        bossHealthBarUI.gameObject.SetActive(true);
        isPlayerInRoom = true;
        core.Movement.SetFacingDirection(-1);
    }

    public bool CheckPlayerInRoom()
    {
        return isPlayerInRoom;
    }

    protected override void HandleDeath()
    {
        stateMachine.ChangeState(deadState);
    }

    protected override void HandleHealthDecrease()
    {
        if (!isPhaseChange && core.Stats.Health.CurrentValue <= core.Stats.Health.MaxValue / 2)
        {
            isPhaseChange = true;
            stateMachine.ChangeState(phaseChangeState);
        }
    }
    
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
