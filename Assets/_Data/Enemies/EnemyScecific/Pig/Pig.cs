using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pig : EnemyStateManager
{
    #region State Variables
    protected PigIdleState idleState;
    public PigIdleState IdleState => idleState;

    protected PigMoveState moveState;
    public PigMoveState MoveState => moveState;

    protected PigDetectedPlayerState detectedPlayerState;
    public PigDetectedPlayerState DetectedPlayerState => detectedPlayerState;

    protected PigChaseState chaseState;
    public PigChaseState ChaseState => chaseState;

    protected PigLookForPlayerState lookForPlayerState;
    public PigLookForPlayerState LookForPlayerState => lookForPlayerState;

    protected PigMeleeAttackState meleeAttackState;
    public PigMeleeAttackState MeleeAttackState => meleeAttackState;

    protected PigStunState stunState;

    protected PigDeadState deadState;
    #endregion

    [Header("Pig")]
    
    [SerializeField] protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] protected EnemyChaseStateSO chaseDataSO;

    protected override void Awake()
    {
        base.Awake();

        idleState = new PigIdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        moveState = new PigMoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        detectedPlayerState = new PigDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, audioDataSO, this);
        chaseState = new PigChaseState(this, stateMachine, "chase", enemyDataSO, audioDataSO, chaseDataSO, this);
        lookForPlayerState = new PigLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, audioDataSO, this);
        meleeAttackState = new PigMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO, meleeAttackPosition, meleeAttackDataSO, this);
        stunState = new PigStunState(this, stateMachine, "stun", enemyDataSO, audioDataSO, this);
        deadState = new PigDeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
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
        if(core.Stats.Health.CurrentValue > 0)
            stateMachine.ChangeState(stunState);
    }
    
    protected override void HandleDeath()
    {
        stateMachine.ChangeState(deadState);
    }
    
    protected override void HandleHealthDecrease()
    {
        AudioManager.Instance.PlaySFX(audioDataSO.hitClip);
        if(stateMachine.CurrentState == stunState) return;
        Flash();
    }
    
    protected override void HandleParry()
    {
        stateMachine.ChangeState(stunState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    }
}
