using UnityEngine;

public class NightBorne : EnemyStateManager
{
    #region State Variables

    protected NightBorneIdleState idleState;
    public NightBorneIdleState IdleState => idleState;

    protected NightBorneMoveState moveState;
    public NightBorneMoveState MoveState => moveState;

    protected NightBorneDetectedPlayerState detectedPlayerState;
    public NightBorneDetectedPlayerState DetectedPlayerState => detectedPlayerState;

    protected NightBorneChaseState chaseState;
    public NightBorneChaseState ChaseState => chaseState;

    protected NightBorneLookForPlayerState lookForPlayerState;
    public NightBorneLookForPlayerState LookForPlayerState => lookForPlayerState;

    protected NightBorneMeleeAttackState meleeAttackState;
    public NightBorneMeleeAttackState MeleeAttackState => meleeAttackState;

    protected NightBorneDeadState deadState;
    public NightBorneDeadState DeadState => deadState;

    #endregion

    [Header("NightBorne")] [SerializeField]
    protected EnemyMeleeAttackStateSO meleeAttackDataSO;
    [SerializeField] EnemyChaseStateSO chaseDataSO;

    protected override void Awake()
    {
        base.Awake();

        idleState = new NightBorneIdleState(this, stateMachine, "idle", enemyDataSO, audioDataSO, this);
        moveState = new NightBorneMoveState(this, stateMachine, "move", enemyDataSO, audioDataSO, this);
        detectedPlayerState =
            new NightBorneDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, audioDataSO, this);
        chaseState = new NightBorneChaseState(this, stateMachine, "chase", enemyDataSO, audioDataSO, chaseDataSO, this);
        lookForPlayerState =
            new NightBorneLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, audioDataSO, this);
        meleeAttackState = new NightBorneMeleeAttackState(this, stateMachine, "meleeAttack", enemyDataSO, audioDataSO,
            meleeAttackPosition, meleeAttackDataSO, this);
        deadState = new NightBorneDeadState(this, stateMachine, "dead", enemyDataSO, audioDataSO, this);
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
        meleeAttackDataSO = Resources.Load<EnemyMeleeAttackStateSO>("Enemies/NightBorne/NightBorneMeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackDataSO", gameObject);
    }

    protected void LoadChaseDataSO()
    {
        if (chaseDataSO != null) return;
        chaseDataSO = Resources.Load<EnemyChaseStateSO>("Enemies/NightBorne/NightBorneChase");
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
}
