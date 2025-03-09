using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NhoxBehaviour
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

    #endregion

    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    [SerializeField] protected CapsuleCollider2D col;
    public CapsuleCollider2D Col => col;

    [Header("Data")]
    [SerializeField] protected PlayerDataSO playerDataSO;

    [SerializeField] protected Vector2 wordSpace;
    [SerializeField] protected Vector2 currentVelocity;
    public Vector2 CurrentVelocity => currentVelocity;
    [SerializeField] protected int facingDirection;
    public int FacingDirection => facingDirection;

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
    }

    protected override void Start()
    {
        base.Start();
        facingDirection = 1;
        stateMachine.Initialize(playerIdleState);
    }

    private void Update()
    {
        currentVelocity = rb.velocity;
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
        LoadRigidbody2d();
        LoadCollider2d();
        LoadPlayerDataSO();
        LoadDashDirectionIndicator();
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
        wordSpace.Set(col.size.x, height);

        center.y += (height - col.size.y) / 2;

        col.size = wordSpace;
        col.offset = center;
    }

    public void SetVelocityX(float velocity)
    {
        wordSpace.Set(velocity, currentVelocity.y);
        rb.velocity = wordSpace;
        currentVelocity = wordSpace;
    }

    public void SetVelocityY(float velocity)
    {
        wordSpace.Set(currentVelocity.x, velocity);
        rb.velocity = wordSpace;
        currentVelocity = wordSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        wordSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = wordSpace;
        currentVelocity = wordSpace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        wordSpace = direction * velocity;
        rb.velocity = wordSpace;
        currentVelocity = wordSpace;
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    public Vector2 DetermineCorner()
    {
        return PlayerCtrl.Instance.TouchingDirection.DetermineLedgePos(wordSpace, facingDirection);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    protected void Flip()
    {
        facingDirection *= -1;
        transform.parent.Rotate(0, 180, 0);
    }
}
