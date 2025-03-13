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

    protected override void Start()
    {
        base.Start();

        archerIdleState = new ArcherIdleState(this, stateMachine, "idle", enemyDataSO, this);
        archerMoveState = new ArcherMoveState(this, stateMachine, "move", enemyDataSO, this);
        archerDetectedPlayerState = new ArcherDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, this);
        archerMeleeAttackState = new ArcherMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, meleeAttackPosition, this);
        archerLookForPlayerState = new ArcherLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, this);
        archerStunState = new ArcherStunState(this, stateMachine, "stun", enemyDataSO, this);
        archerDeadState = new ArcherDeadState(this, stateMachine, "dead", enemyDataSO, this);
        archerDodgeState = new ArcherDodgeState(this, stateMachine, "dodge", enemyDataSO, this);
        archerRangedAttackState = new ArcherRangedAttackState(this, stateMachine, "rangedAttack", enemyDataSO, rangedAttackPosition, this);

        stateMachine.Initialize(archerMoveState);
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

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(archerDeadState);
        }
        else if (isStunned && stateMachine.CurrentState != archerStunState)
        {
            stateMachine.ChangeState(archerStunState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(archerRangedAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            archerLookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(archerLookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, enemyDataSO.attackRadius);
    }
}
