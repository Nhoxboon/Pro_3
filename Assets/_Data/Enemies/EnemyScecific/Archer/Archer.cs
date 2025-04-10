using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
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

    [SerializeField] Transform meleeAttackPosition;
    [SerializeField] Transform rangedAttackPosition;

    protected override void Awake()
    {
        base.Awake();

        archerIdleState = new ArcherIdleState(this, stateMachine, "idle", enemyDataSO, this);
        archerMoveState = new ArcherMoveState(this, stateMachine, "move", enemyDataSO, this);
        archerDetectedPlayerState = new ArcherDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, this);
        archerMeleeAttackState = new ArcherMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, meleeAttackPosition, this);
        archerLookForPlayerState = new ArcherLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, this);
        archerStunState = new ArcherStunState(this, stateMachine, "stun", enemyDataSO, this);
        archerDeadState = new ArcherDeadState(this, stateMachine, "dead", enemyDataSO, this);
        archerDodgeState = new ArcherDodgeState(this, stateMachine, "dodge", enemyDataSO, this);
        archerRangedAttackState = new ArcherRangedAttackState(this, stateMachine, "rangedAttack", enemyDataSO, rangedAttackPosition, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(archerMoveState);
    }

    protected void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttackPosition();
        LoadRangedAttackPosition();
    }

    protected void LoadMeleeAttackPosition()
    {
        if (meleeAttackPosition != null) return;
        meleeAttackPosition = transform.parent.Find("Attack/MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackPosition", gameObject);
    }

    protected void LoadRangedAttackPosition()
    {
        if (rangedAttackPosition != null) return;
        rangedAttackPosition = transform.parent.Find("Attack/RangedAttack");
        Debug.Log(transform.name + " LoadRangedAttackPosition", gameObject);
    }
    
    protected override void HandleParry()
    {
        base.HandleParry();
        
        stateMachine.ChangeState(archerStunState);
    }

    protected void HandlePoiseZero()
    {
        stateMachine.ChangeState(archerStunState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, enemyDataSO.attackRadius);
    }
}
