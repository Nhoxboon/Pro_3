
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : EnemyStateManager
{
    #region State Variables
    
    protected Boss_1IntroState bossIntroState;
    public Boss_1IntroState BossIntroState => bossIntroState;

    protected Boss_1IdleState bossIdleState;
    public Boss_1IdleState BossIdleState => bossIdleState;
    
    protected Boss_1MoveState bossMoveState;
    public Boss_1MoveState BossMoveState => bossMoveState;
    
    protected Boss_1MeleeAttackState bossMeleeAttackState;
    public Boss_1MeleeAttackState BossMeleeAttackState => bossMeleeAttackState;

    protected Boss_1RangedAttackState bossRangedAttackState;
    public Boss_1RangedAttackState BossRangedAttackState => bossRangedAttackState;
    
    protected Boss_1MoveByPointState bossMoveByPointState;
    public Boss_1MoveByPointState BossMoveByPointState => bossMoveByPointState;
    
    protected Boss_1PhaseChangeState bossPhaseChangeState;

    protected Boss_1DeadState bossDeadState;
    #endregion
    
    [Header("Boss 1")]
    [SerializeField] protected Transform rangedAttackPosition;
    
    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected BossRangedAttackStateSO rangedAttackDataSO;
    public BossRangedAttackStateSO RangedAttackStateSO => rangedAttackDataSO;
    
    [Header("Move Points")]
    [SerializeField] protected Transform pointsHolder;
    [SerializeField] protected List<Transform> movePoints;
    public List<Transform> MovePoints => movePoints;
    
    public float lastMoveByPointTime;
    
    protected bool isPhaseChange = false;
    public bool IsPhaseChange => isPhaseChange;

    protected override void Awake()
    {
        base.Awake();
        
        bossIntroState = new Boss_1IntroState(this, stateMachine, "intro", enemyDataSO, audioDataSO, this);
        bossIdleState = new Boss_1IdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        bossMoveState = new Boss_1MoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        bossMeleeAttackState = new Boss_1MeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO, meleeAttackPosition, meleeAttackDataSO, this);
        bossRangedAttackState = new Boss_1RangedAttackState(this, stateMachine, "rangedAttack", enemyDataSO, audioDataSO, rangedAttackPosition, rangedAttackDataSO, this);
        bossMoveByPointState = new Boss_1MoveByPointState(this, stateMachine, "move", enemyDataSO, audioDataSO, movePoints, this);
        bossPhaseChangeState = new Boss_1PhaseChangeState(this, stateMachine, "phaseChange", enemyDataSO, audioDataSO, this);
        bossDeadState = new Boss_1DeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(bossIntroState);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRangedAttackPosition();
        LoadMeleeAttackDataSO();
        LoadRangedAttackDataSO();
        LoadPoints();
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
    
    protected void LoadRangedAttackPosition()
    {
        if (rangedAttackPosition != null) return;
        rangedAttackPosition = transform.Find("Attack/RangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackPosition", gameObject);
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
    #endregion

    protected override void HandleParry()
    {
        
    }

    protected override void HandleDeath()
    {
        stateMachine.ChangeState(bossDeadState);
    }

    protected override void HandlePoiseZero()
    {
        
    }

    protected override void HandleHealthDecrease()
    {
        if (!isPhaseChange && core.Stats.Health.CurrentValue <= core.Stats.Health.MaxValue / 2)
        {
            isPhaseChange = true;
            stateMachine.ChangeState(bossPhaseChangeState);
        }
    }
    
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
