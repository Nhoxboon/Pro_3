using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : NhoxBehaviour
{
    [SerializeField] protected float groundCheckRadius = 0.3f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform groundCheck;

    [SerializeField] protected bool isGrounded;
    public bool IsGrounded => isGrounded;

    [SerializeField] protected bool isTouchingWall;
    public bool IsTouchingWall => isTouchingWall;

    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.65f;
    public float WallCheckDistance => wallCheckDistance;

    [SerializeField] protected Transform ledgeCheck;
    [SerializeField] protected bool isTouchingLedge;
    public bool IsTouchingLedge => isTouchingLedge;
    [SerializeField] protected bool ledgeDetected;
    public bool LedgeDetected => ledgeDetected;
    [SerializeField] protected Vector2 ledgePosBot;
    public Vector2 LedgePosBot => ledgePosBot;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGroundCheck();
        LoadWallCheck();
        LoadLedgeCheck();
        LoadLayer();
    }

    protected void LoadLayer()
    {
        if(whatIsGround != 0) return;
        this.whatIsGround = LayerMask.GetMask("Ground");
        Debug.Log(transform.name + " LoadLayer", gameObject);
    }

    protected void LoadGroundCheck()
    {
        if (groundCheck != null) return;
        this.groundCheck = transform.Find("GroundCheck");
        Debug.Log(transform.name + " LoadGroundCheck", gameObject);
    }

    protected void LoadWallCheck()
    {
        if (wallCheck != null) return;
        this.wallCheck = transform.Find("WallCheck");
        Debug.Log(transform.name + " LoadWallCheck", gameObject);
    }

    protected void LoadLedgeCheck()
    {
        if (ledgeCheck != null) return;
        this.ledgeCheck = transform.Find("LedgeCheck");
        Debug.Log(transform.name + " LoadLedgeCheck", gameObject);
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    protected void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.parent.right, wallCheckDistance, whatIsGround);

        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.parent.right, wallCheckDistance, whatIsGround);

        if (isTouchingWall && !isGrounded && !isTouchingLedge)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }

    public bool CheckTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, -transform.parent.right, wallCheckDistance, whatIsGround);
    }


    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

        Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));
    }

    public override void Reset()
    {
        this.LoadComponents();
        ledgeDetected = false;
    }
}
