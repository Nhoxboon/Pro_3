using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pig : EnemyStateManager
{
    #region State Variables
    protected PigIdleState pigIdleState;
    public PigIdleState PigIdleState => pigIdleState;

    protected PigMoveState pigMoveState;
    public PigMoveState PigMoveState => pigMoveState;

    protected PigDetectedPlayerState pigDetectedPlayerState;
    public PigDetectedPlayerState PigDetectedPlayerState => pigDetectedPlayerState;

    protected PigChaseState pigChaseState;
    public PigChaseState PigChaseState => pigChaseState;

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
    
    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected EnemyChaseStateSO chaseDataSO;

    protected override void Awake()
    {
        base.Awake();

        pigIdleState = new PigIdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        pigMoveState = new PigMoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        pigDetectedPlayerState = new PigDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, audioDataSO, this);
        pigChaseState = new PigChaseState(this, stateMachine, "charge", enemyDataSO, audioDataSO, chaseDataSO, this);
        pigLookForPlayerState = new PigLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, audioDataSO, this);
        pigMeleeAttackState = new PigMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO, meleeAttackPosition, meleeAttackDataSO, this);
        pigStunState = new PigStunState(this, stateMachine, "stun", enemyDataSO, audioDataSO, this);
        pigDeadState = new PigDeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);
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
        LoadChaseDataSO();
    }

    protected void LoadMeleeAttackDataSO()
    {
        if(meleeAttackDataSO != null) return;
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Enemies/Pig/PigMeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }
    
    protected void LoadChaseDataSO()
    {
        if (chaseDataSO != null) return;
        chaseDataSO = Resources.Load<EnemyChaseStateSO>("Enemies/Pig/PigChase");
        Debug.Log(transform.name + " LoadChaseDataSO", gameObject);
    }

    protected override void HandlePoiseZero()
    {
        stateMachine.ChangeState(pigStunState);
    }
    
    protected override void HandleDeath()
    {
        stateMachine.ChangeState(pigDeadState);
    }
    
    protected override void HandleHealthDecrease()
    {
        AudioManager.Instance.PlaySFX(audioDataSO.hitClip);
        if(stateMachine.CurrentState == pigStunState) return;
        Flash();
    }
    
    protected override void HandleParry()
    {
        stateMachine.ChangeState(pigStunState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
