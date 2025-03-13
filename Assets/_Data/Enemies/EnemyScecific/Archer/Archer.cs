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

    [SerializeField] Transform meleeAttackPosition;

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

        stateMachine.Initialize(archerMoveState);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttackPosition();
    }

    protected void LoadMeleeAttackPosition()
    {
        if (meleeAttackPosition != null) return;
        meleeAttackPosition = transform.parent.Find("Attack/MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackPosition", gameObject);
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
