using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    protected PigIdleState pigIdleState;
    public PigIdleState PigIdleState => pigIdleState;

    protected PigMoveState pigMoveState;
    public PigMoveState PigMoveState => pigMoveState;

    protected PigDetectedPlayerState pigDetectedPlayerState;
    public PigDetectedPlayerState PigDetectedPlayerState => pigDetectedPlayerState;

    protected PigChargeState pigChargeState;
    public PigChargeState PigChargeState => pigChargeState;

    protected PigLookForPlayerState pigLookForPlayerState;
    public PigLookForPlayerState PigLookForPlayerState => pigLookForPlayerState;

    protected PigMeleeAttackState pigMeleeAttackState;
    public PigMeleeAttackState PigMeleeAttackState => pigMeleeAttackState;

    protected PigStunState pigStunState;
    public PigStunState PigStunState => pigStunState;

    protected PigDeadState pigDeadState;
    public PigDeadState PigDeadState => pigDeadState;

    [SerializeField] Transform meleeAttackPosition;

    protected override void Start()
    {
        base.Start();

        pigIdleState = new PigIdleState(this, stateMachine, "idle", enemyDataSO, this);
        pigMoveState = new PigMoveState(this, stateMachine, "move", enemyDataSO, this);
        pigDetectedPlayerState = new PigDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, this);
        pigChargeState = new PigChargeState(this, stateMachine, "charge", enemyDataSO, this);
        pigLookForPlayerState = new PigLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, this);
        pigMeleeAttackState = new PigMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, meleeAttackPosition, this);
        pigStunState = new PigStunState(this, stateMachine, "stun", enemyDataSO, this);
        pigDeadState = new PigDeadState(this, stateMachine, "dead", enemyDataSO, this);

        stateMachine.Initialize(pigMoveState);
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

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, enemyDataSO.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(pigDeadState);
        }
        else if (isStunned && stateMachine.CurrentState != pigStunState)
        {
            stateMachine.ChangeState(pigStunState);
        }

        
    }
}
