using UnityEngine;

public class Archer : EnemyStateManager
{
    #region State Variables

    protected ArcherIdleState archerIdleState;
    public ArcherIdleState ArcherIdleState => archerIdleState;

    protected ArcherMoveState archerMoveState;
    public ArcherMoveState ArcherMoveState => archerMoveState;

    protected ArcherDetectedPlayerState archerDetectedPlayerState;
    public ArcherDetectedPlayerState ArcherDetectedPlayerState => archerDetectedPlayerState;

    protected ArcherMeleeAttackState archerMeleeAttackState;
    public ArcherMeleeAttackState ArcherMeleeAttackState => archerMeleeAttackState;

    protected ArcherLookForPlayerState archerLookForPlayerState;
    public ArcherLookForPlayerState ArcherLookForPlayerState => archerLookForPlayerState;

    protected ArcherStunState archerStunState;
    public ArcherStunState ArcherStunState => archerStunState;

    protected ArcherDeadState archerDeadState;
    public ArcherDeadState ArcherDeadState => archerDeadState;

    protected ArcherDodgeState archerDodgeState;
    public ArcherDodgeState ArcherDodgeState => archerDodgeState;

    protected ArcherRangedAttackState archerRangedAttackState;
    public ArcherRangedAttackState ArcherRangedAttackState => archerRangedAttackState;

    #endregion
    [Header("Archer")] 

    [SerializeField] private Transform rangedAttackPosition;

    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected EnemyRangedAttackStateSO rangedAttackDataSO;
    [SerializeField] protected EnemyDodgeStateSO dodgeDataSO;
    public EnemyDodgeStateSO DodgeDataSO => dodgeDataSO;

    protected override void Awake()
    {
        base.Awake();

        archerIdleState = new ArcherIdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        archerMoveState = new ArcherMoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        archerDetectedPlayerState =
            new ArcherDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, audioDataSO, this);
        archerMeleeAttackState = new ArcherMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO,
            meleeAttackPosition, meleeAttackDataSO, this);
        archerLookForPlayerState =
            new ArcherLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, audioDataSO, this);
        archerStunState = new ArcherStunState(this, stateMachine, "stun", enemyDataSO, audioDataSO, this);
        archerDeadState = new ArcherDeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);
        archerDodgeState =
            new ArcherDodgeState(this, stateMachine, "dodge", enemyDataSO, audioDataSO, dodgeDataSO, this);
        archerRangedAttackState = new ArcherRangedAttackState(this, stateMachine, "rangedAttack", enemyDataSO,
            audioDataSO, rangedAttackPosition, rangedAttackDataSO, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(archerMoveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttackDataSO();
        LoadRangedAttackDataSO();
        LoadDodgeDataSO();
        LoadMeleeAttackPosition();
        LoadRangedAttackPosition();
    }
    
    protected void LoadMeleeAttackDataSO()
    {
        if (meleeAttackDataSO != null) return;
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Enemies/Archer/ArcherMeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }

    protected void LoadRangedAttackDataSO()
    {
        if (rangedAttackDataSO != null) return;
        rangedAttackDataSO = Resources.Load<EnemyRangedAttackStateSO>("Enemies/Archer/ArcherRangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackDataSO", gameObject);
    }

    protected void LoadDodgeDataSO()
    {
        if (dodgeDataSO != null) return;
        dodgeDataSO = Resources.Load<EnemyDodgeStateSO>("Enemies/Archer/ArcherDodge");
        Debug.Log(transform.name + " LoadDodgeDataSO", gameObject);
    }

    protected void LoadRangedAttackPosition()
    {
        if (rangedAttackPosition != null) return;
        rangedAttackPosition = transform.Find("Attack/RangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackPosition", gameObject);
    }

    protected override void HandleParry()
    {
        stateMachine.ChangeState(archerStunState);
    }
    
    protected override void HandleDeath()
    {
        stateMachine.ChangeState(archerDeadState);
    }

    protected override void HandlePoiseZero()
    {
        stateMachine.ChangeState(archerStunState);
    }

    protected override void HandleHealthDecrease()
    {
        AudioManager.Instance.PlaySFX(audioDataSO.hitClip);
        if (stateMachine.CurrentState == archerStunState) return;
        Flash();
    }
}