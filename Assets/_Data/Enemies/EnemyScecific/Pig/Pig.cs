using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyStateManager
{
    #region State Variables
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
    #endregion

    [Header("Pig")]
    [SerializeField] Transform meleeAttackPosition;
    
    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected EnemyChargeStateSO chargeDataSO;

    protected override void Awake()
    {
        base.Awake();

        pigIdleState = new PigIdleState(this, stateMachine, "idle", enemyDataSO, this);
        pigMoveState = new PigMoveState(this, stateMachine, "move", enemyDataSO, this);
        pigDetectedPlayerState = new PigDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, this);
        pigChargeState = new PigChargeState(this, stateMachine, "charge", enemyDataSO, chargeDataSO, this);
        pigLookForPlayerState = new PigLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, this);
        pigMeleeAttackState = new PigMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, meleeAttackPosition, meleeAttackDataSO, this);
        pigStunState = new PigStunState(this, stateMachine, "stun", enemyDataSO, this);
        pigDeadState = new PigDeadState(this, stateMachine, "dead", enemyDataSO, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(pigMoveState);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttackDataSO();
        LoadChargeDataSO();
        LoadMeleeAttackPosition();
    }

    protected void LoadMeleeAttackDataSO()
    {
        if(meleeAttackDataSO != null) return;
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Enemies/Pig/PigMeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }
    
    protected void LoadChargeDataSO()
    {
        if (chargeDataSO != null) return;
        chargeDataSO = Resources.Load<EnemyChargeStateSO>("Enemies/Pig/PigCharge");
        Debug.Log(transform.name + " LoadChargeDataSO", gameObject);
    }
    
    protected void LoadMeleeAttackPosition()
    {
        if (meleeAttackPosition != null) return;
        meleeAttackPosition = transform.Find("Attack/MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackPosition", gameObject);
    }

    protected override void HandlePoiseZero()
    {
        base.HandlePoiseZero();
        stateMachine.ChangeState(pigStunState);
    }
    
    protected override void HandleHealthDecrease()
    {
        base.HandleHealthDecrease();
        if(stateMachine.CurrentState == pigStunState) return;
        Flash();
    }
    
    protected override void HandleParry()
    {
        base.HandleParry();
        stateMachine.ChangeState(pigStunState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
