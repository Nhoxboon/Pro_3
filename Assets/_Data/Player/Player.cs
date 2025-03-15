using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NhoxBehaviour
{
    #region State Variable
    protected PlayerStateMachine stateMachine;
    public PlayerStateMachine StateMachine => stateMachine;

    protected PlayerIdleState playerIdleState;
    public PlayerIdleState PlayerIdleState => playerIdleState;

    protected PlayerMoveState playerMoveState;
    public PlayerMoveState PlayerMoveState => playerMoveState;

    protected PlayerJumpState playerJumpState;
    public PlayerJumpState PlayerJumpState => playerJumpState;

    protected PlayerInAirState playerInAirState;
    public PlayerInAirState PlayerInAirState => playerInAirState;

    protected PlayerLandState playerLandState;
    public PlayerLandState PlayerLandState => playerLandState;

    protected PlayerWallSlideState playerWallSlideState;
    public PlayerWallSlideState PlayerWallSlideState => playerWallSlideState;

    protected PlayerWallGrabState playerWallGrabState;
    public PlayerWallGrabState PlayerWallGrabState => playerWallGrabState;

    protected PlayerWallClimbState playerWallClimbState;
    public PlayerWallClimbState PlayerWallClimbState => playerWallClimbState;

    protected PlayerWallJumpState playerWallJumpState;
    public PlayerWallJumpState PlayerWallJumpState => playerWallJumpState;

    protected PlayerLedgeClimbState playerLedgeClimbState;
    public PlayerLedgeClimbState PlayerLedgeClimbState => playerLedgeClimbState;

    protected PlayerDashState playerDashState;
    public PlayerDashState PlayerDashState => playerDashState;

    protected PlayerCrouchIdleState playerCrouchIdleState;
    public PlayerCrouchIdleState PlayerCrouchIdleState => playerCrouchIdleState;

    protected PlayerCrouchMoveState playerCrouchMoveState;
    public PlayerCrouchMoveState PlayerCrouchMoveState => playerCrouchMoveState;

    protected PlayerAttackState primaryAttackState;
    public PlayerAttackState PrimaryAttackState => primaryAttackState;

    protected PlayerAttackState secondaryAttackState;
    public PlayerAttackState SecondaryAttackState => secondaryAttackState;


    #endregion

    [Header("Component")]

    [SerializeField] protected Core core;
    public Core Core => core;

    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    [SerializeField] protected CapsuleCollider2D col;
    public CapsuleCollider2D Col => col;

    [Header("Data")]
    [SerializeField] protected PlayerDataSO playerDataSO;

    [SerializeField] protected Vector2 workpace;

    [SerializeField] protected Transform dashDirectionIndicator;
    public Transform DashDirectionIndicator => dashDirectionIndicator;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(this, stateMachine, playerDataSO, "idle");
        playerMoveState = new PlayerMoveState(this, stateMachine, playerDataSO, "move");
        playerJumpState = new PlayerJumpState(this, stateMachine, playerDataSO, "inAir");
        playerInAirState = new PlayerInAirState(this, stateMachine, playerDataSO, "inAir");
        playerLandState = new PlayerLandState(this, stateMachine, playerDataSO, "land");
        playerWallSlideState = new PlayerWallSlideState(this, stateMachine, playerDataSO, "wallSlide");
        playerWallGrabState = new PlayerWallGrabState(this, stateMachine, playerDataSO, "wallGrab");
        playerWallClimbState = new PlayerWallClimbState(this, stateMachine, playerDataSO, "wallClimb");
        playerWallJumpState = new PlayerWallJumpState(this, stateMachine, playerDataSO, "inAir");
        playerLedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerDataSO, "ledgeClimbState");
        playerDashState = new PlayerDashState(this, stateMachine, playerDataSO, "inAir");
        playerCrouchIdleState = new PlayerCrouchIdleState(this, stateMachine, playerDataSO, "crouchIdle");
        playerCrouchMoveState = new PlayerCrouchMoveState(this, stateMachine, playerDataSO, "crouchMove");
        primaryAttackState = new PlayerAttackState(this, stateMachine, playerDataSO, "attack");
        secondaryAttackState = new PlayerAttackState(this, stateMachine, playerDataSO, "attack");
    }

    protected override void Start()
    {
        base.Start();

        primaryAttackState.SetWeapon(PlayerCtrl.Instance.PlayerInventory.weapons[(int)CombatInputs.primary]);

        stateMachine.Initialize(playerIdleState);
    }

    private void Update()
    {
        core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCore();
        LoadRigidbody2d();
        LoadCollider2d();
        LoadPlayerDataSO();
        LoadDashDirectionIndicator();
    }

    protected void LoadCore()
    {
        if(this.core != null) return;
        this.core = transform.parent.GetComponentInChildren<Core>();
        Debug.Log(transform.name + " LoadCore", gameObject);
    }

    protected void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2d", gameObject);
    }

    protected void LoadCollider2d()
    {
        if (this.col != null) return;
        this.col = GetComponentInParent<CapsuleCollider2D>();
        Debug.Log(transform.name + " LoadCollider2d", gameObject);
    }

    protected void LoadPlayerDataSO()
    {
        if (this.playerDataSO != null) return;
        this.playerDataSO = Resources.Load<PlayerDataSO>("Player");
        Debug.Log(transform.name + " LoadPlayerDataSO", gameObject);
    }

    protected void LoadDashDirectionIndicator()
    {
        if(this.dashDirectionIndicator != null) return;
        this.dashDirectionIndicator = transform.parent.Find("DashDirectionIndicator");
        Debug.Log(transform.name + " LoadDashDirectionIndicator", gameObject);
    }
    #endregion

    public void SetColliderHeight(float height)
    {
        Vector2 center = col.offset;
        workpace.Set(col.size.x, height);

        center.y += (height - col.size.y) / 2;

        col.size = workpace;
        col.offset = center;
    }

    

    public void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    
    
}
