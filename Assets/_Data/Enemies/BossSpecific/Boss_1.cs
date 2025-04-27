
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

    #endregion
    
    [Header("Boss 1")]
    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;

    protected override void Awake()
    {
        base.Awake();
        
        bossIntroState = new Boss_1IntroState(this, stateMachine, "intro", enemyDataSO, audioDataSO, this);
        bossIdleState = new Boss_1IdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        bossMoveState = new Boss_1MoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        bossMeleeAttackState = new Boss_1MeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO, meleeAttackPosition, meleeAttackDataSO, this);
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(bossIntroState);
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttackDataSO();
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

    protected override void HandleParry()
    {
        
    }

    protected override void HandleDeath()
    {
        
    }

    protected override void HandlePoiseZero()
    {
        
    }

    protected override void HandleHealthDecrease()
    {
        
    }
    
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
