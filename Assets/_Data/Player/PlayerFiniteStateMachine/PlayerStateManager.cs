using System.Collections;
using UnityEngine;

public class PlayerStateManager : NhoxBehaviour
{
    #region State Variable

    protected PlayerStateMachine stateMachine;

    protected PlayerIdleState playerIdleState;
    public PlayerIdleState PlayerIdleState => playerIdleState;

    protected PlayerMoveState playerMoveState;
    public PlayerMoveState PlayerMoveState => playerMoveState;

    protected PlayerStunState playerStunState;
    public PlayerStunState PlayerStunState => playerStunState;

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

    protected PlayerDeadState playerDeadState;

    protected PlayerAttackState primaryAttackState;
    public PlayerAttackState PrimaryAttackState => primaryAttackState;

    protected PlayerAttackState secondaryAttackState;
    public PlayerAttackState SecondaryAttackState => secondaryAttackState;

    #endregion
    
    [Header("Component")] [SerializeField] protected Core core;

    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected CapsuleCollider2D col;

    [SerializeField] protected Vector2 workpace;

    [SerializeField] protected Weapon primaryWeapon;

    [SerializeField] protected Weapon secondaryWeapon;

    [Header("Data")] [SerializeField] protected PlayerDataSO playerDataSO;

    [SerializeField] protected PlayerAudioDataSO playerAudioDataSO;

    [Header("Interactable")] [SerializeField]
    protected InteractableDetector interactableDetector;

    public Core Core => core;
    public Rigidbody2D Rb => rb;
    public PlayerAudioDataSO PlayerAudioDataSO => playerAudioDataSO;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(this, stateMachine, playerDataSO, playerAudioDataSO, "idle");
        playerMoveState = new PlayerMoveState(this, stateMachine, playerDataSO, playerAudioDataSO, "move");
        playerStunState = new PlayerStunState(this, stateMachine, playerDataSO, playerAudioDataSO, "stun");
        playerJumpState = new PlayerJumpState(this, stateMachine, playerDataSO, playerAudioDataSO, "inAir");
        playerInAirState = new PlayerInAirState(this, stateMachine, playerDataSO, playerAudioDataSO, "inAir");
        playerLandState = new PlayerLandState(this, stateMachine, playerDataSO, playerAudioDataSO, "land");
        playerWallSlideState =
            new PlayerWallSlideState(this, stateMachine, playerDataSO, playerAudioDataSO, "wallSlide");
        playerWallGrabState = new PlayerWallGrabState(this, stateMachine, playerDataSO, playerAudioDataSO, "wallGrab");
        playerWallClimbState =
            new PlayerWallClimbState(this, stateMachine, playerDataSO, playerAudioDataSO, "wallClimb");
        playerWallJumpState = new PlayerWallJumpState(this, stateMachine, playerDataSO, playerAudioDataSO, "inAir");
        playerLedgeClimbState =
            new PlayerLedgeClimbState(this, stateMachine, playerDataSO, playerAudioDataSO, "ledgeClimbState");
        playerDashState = new PlayerDashState(this, stateMachine, playerDataSO, playerAudioDataSO, "inAir");
        playerCrouchIdleState =
            new PlayerCrouchIdleState(this, stateMachine, playerDataSO, playerAudioDataSO, "crouchIdle");
        playerCrouchMoveState =
            new PlayerCrouchMoveState(this, stateMachine, playerDataSO, playerAudioDataSO, "crouchMove");
        playerDeadState = new PlayerDeadState(this, stateMachine, playerDataSO, playerAudioDataSO, "dead");
        primaryAttackState = new PlayerAttackState(this, stateMachine, playerDataSO, playerAudioDataSO, "attack",
            primaryWeapon, CombatInputs.primary);
        secondaryAttackState = new PlayerAttackState(this, stateMachine, playerDataSO, playerAudioDataSO, "attack",
            secondaryWeapon, CombatInputs.secondary);
    }

    protected override void Start()
    {
        base.Start();

        InputManager.Instance.OnInteractInputChanged += interactableDetector.TryInteract;

        core.Stats.Health.OnCurrentValueZero += HandleDeath;
        core.Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;
        core.Stats.Health.OnValueDecreased += HandleHealthDecrease;
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

    protected void OnDestroy()
    {
        core.Stats.Health.OnCurrentValueZero -= HandleDeath;
        core.Stats.Poise.OnCurrentValueZero -= HandlePoiseCurrentValueZero;
        core.Stats.Health.OnValueDecreased -= HandleHealthDecrease;
    }

    public void SetColliderHeight(float height)
    {
        var center = col.offset;
        workpace.Set(col.size.x, height);

        center.y += (height - col.size.y) / 2;

        col.size = workpace;
        col.offset = center;
    }

    protected void HandleHealthDecrease()
    {
        AudioManager.Instance.PlaySFX(playerAudioDataSO.hitClip);
        if (stateMachine.CurrentState == playerStunState) return;
        Flash();
    }

    protected void HandleDeath()
    {
        stateMachine.ChangeState(playerDeadState);
    }

    protected void HandlePoiseCurrentValueZero()
    {
        stateMachine.ChangeState(playerStunState);
    }

    protected void Flash()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        PlayerCtrl.Instance.Sr.material.SetInt("_Flash", 1);
        yield return new WaitForSeconds(0.3f);
        PlayerCtrl.Instance.Sr.material.SetInt("_Flash", 0);
    }

    public void AnimationTrigger()
    {
        stateMachine.CurrentState.AnimationTrigger();
    }

    public void AnimationFinishTrigger()
    {
        stateMachine.CurrentState.AnimationFinishTrigger();
    }

   

    #region Load Components

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCore();
        LoadRigidbody2d();
        LoadCollider2d();
        LoadPlayerDataSO();
        LoadPlayerAudioDataSO();
        LoadPrimaryWeapon();
        LoadSecondaryWeapon();
        LoadInteractableDetector();
    }

    protected void LoadCore()
    {
        if (core != null) return;
        core = transform.GetComponentInChildren<Core>();
        Debug.Log(transform.name + " LoadCore", gameObject);
    }

    protected void LoadRigidbody2d()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2d", gameObject);
    }

    protected void LoadCollider2d()
    {
        if (col != null) return;
        col = GetComponentInParent<CapsuleCollider2D>();
        Debug.Log(transform.name + " LoadCollider2d", gameObject);
    }

    protected void LoadPlayerDataSO()
    {
        if (playerDataSO != null) return;
        playerDataSO = Resources.Load<PlayerDataSO>("Player/Player");
        Debug.Log(transform.name + " LoadPlayerDataSO", gameObject);
    }

    protected void LoadPlayerAudioDataSO()
    {
        if (playerAudioDataSO != null) return;
        playerAudioDataSO = Resources.Load<PlayerAudioDataSO>("Player/PlayerAudio");
        Debug.Log(transform.name + " LoadPlayerAudioDataSO", gameObject);
    }

    protected void LoadPrimaryWeapon()
    {
        if (primaryWeapon != null) return;
        primaryWeapon = transform.Find("Weapons/PrimaryWeapon").GetComponent<Weapon>();
        Debug.Log(transform.name + " LoadPrimaryWeapon", gameObject);
    }

    protected void LoadSecondaryWeapon()
    {
        if (secondaryWeapon != null) return;
        secondaryWeapon = transform.Find("Weapons/SecondaryWeapon").GetComponent<Weapon>();
        Debug.Log(transform.name + " LoadSecondaryWeapon", gameObject);
    }

    protected void LoadInteractableDetector()
    {
        if (interactableDetector != null) return;
        interactableDetector = transform.GetComponentInChildren<InteractableDetector>();
        Debug.Log(transform.name + " LoadInteractableDetector", gameObject);
    }

    #endregion
}