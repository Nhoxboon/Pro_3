using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Transform meleeAttackPosition;
    [SerializeField] Transform rangedAttackPosition;
    
    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected EnemyRangedAttackStateSO rangedAttackDataSO;
    [SerializeField] protected EnemyDodgeStateSO dodgeDataSO;
    public EnemyDodgeStateSO DodgeDataSO => dodgeDataSO;

    protected override void Awake()
    {
        base.Awake();

        archerIdleState = new ArcherIdleState(this, stateMachine, "idle", enemyDataSO, this);
        archerMoveState = new ArcherMoveState(this, stateMachine, "move", enemyDataSO, this);
        archerDetectedPlayerState = new ArcherDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, this);
        archerMeleeAttackState = new ArcherMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, meleeAttackPosition, meleeAttackDataSO, this);
        archerLookForPlayerState = new ArcherLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, this);
        archerStunState = new ArcherStunState(this, stateMachine, "stun", enemyDataSO, this);
        archerDeadState = new ArcherDeadState(this, stateMachine, "dead", enemyDataSO, this);
        archerDodgeState = new ArcherDodgeState(this, stateMachine, "dodge", enemyDataSO, dodgeDataSO, this);
        archerRangedAttackState = new ArcherRangedAttackState(this, stateMachine, "rangedAttack", enemyDataSO, rangedAttackPosition, rangedAttackDataSO, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(archerMoveState);
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
        if(meleeAttackDataSO != null) return;
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Enemies/Archer/ArcherMeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }
    
    protected void LoadRangedAttackDataSO()
    {
        if(rangedAttackDataSO != null) return;
        rangedAttackDataSO = Resources.Load<EnemyRangedAttackStateSO>("Enemies/Archer/ArcherRangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackDataSO", gameObject);
    }
    
    protected void LoadDodgeDataSO()
    {
        if(dodgeDataSO != null) return;
        dodgeDataSO = Resources.Load<EnemyDodgeStateSO>("Enemies/Archer/ArcherDodge");
        Debug.Log(transform.name + " LoadDodgeDataSO", gameObject);
    }

    protected void LoadMeleeAttackPosition()
    {
        if (meleeAttackPosition != null) return;
        meleeAttackPosition = transform.Find("Attack/MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackPosition", gameObject);
    }

    protected void LoadRangedAttackPosition()
    {
        if (rangedAttackPosition != null) return;
        rangedAttackPosition = transform.Find("Attack/RangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackPosition", gameObject);
    }
    
    protected override void HandleParry()
    {
        base.HandleParry();
        
        stateMachine.ChangeState(archerStunState);
    }

    protected override void HandlePoiseZero()
    {
        base.HandlePoiseZero();
        stateMachine.ChangeState(archerStunState);
    }
    
    protected override void HandleHealthDecrease()
    {
        base.HandleHealthDecrease();
        if(stateMachine.CurrentState == archerStunState) return;
        Flash();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
