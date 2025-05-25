using UnityEngine;

public class Skeleton : EnemyStateManager
{
    #region State Variables

    protected SkeletonIdleState idleState;
    public SkeletonIdleState IdleState => idleState;

    protected SkeletonMoveState moveState;
    public SkeletonMoveState MoveState => moveState;

    protected SkeletonDetectedPlayer detectedPlayerState;
    public SkeletonDetectedPlayer DetectedPlayerState => detectedPlayerState;

    protected SkeletonChaseState chaseState;
    public SkeletonChaseState ChaseState => chaseState;

    protected SkeletonLookForPlayerState lookForPlayerState;
    public SkeletonLookForPlayerState LookForPlayerState => lookForPlayerState;

    protected SkeletonMeleeAttackState meleeAttackState;
    public SkeletonMeleeAttackState MeleeAttackState => meleeAttackState;

    protected SkeletonDeadState deadState;

    #endregion

    [Header("Skeleton")] [SerializeField]
    protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] EnemyChaseStateSO chaseDataSO;

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        moveState = new SkeletonMoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        detectedPlayerState = new SkeletonDetectedPlayer(this, stateMachine, "detectedPlayer", enemyDataSO,
            audioDataSO, this);
        chaseState = new SkeletonChaseState(this, stateMachine, "chase", enemyDataSO, audioDataSO, chaseDataSO, this);
        lookForPlayerState =
            new SkeletonLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, audioDataSO, this);
        meleeAttackState = new SkeletonMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO,
            meleeAttackPosition, meleeAttackDataSO, this);
        deadState = new SkeletonDeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);
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
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Enemies/Skeleton/SkeletonMeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }

    protected void LoadChaseDataSO()
    {
        if (chaseDataSO != null) return;
        chaseDataSO = Resources.Load<EnemyChaseStateSO>("Enemies/Skeleton/SkeletonChase");
        Debug.Log(transform.name + " LoadChaseDataSO", gameObject);
    }

    protected override void HandleDeath()
    {
        stateMachine.ChangeState(deadState);
    }

    protected override void HandleHealthDecrease()
    {
        AudioManager.Instance.PlaySFX(audioDataSO.hitClip);
        Flash();
    }

    // public override void OnDrawGizmos()
    // {
    //     base.OnDrawGizmos();
    //     Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackDataSO.attackRadius);
    // }
}
