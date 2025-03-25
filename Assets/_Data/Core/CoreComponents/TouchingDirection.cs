using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : CoreComponent
{
    [Header("Touching Direction")]

    [SerializeField] protected float groundCheckRadius = 0.3f;
    [SerializeField] protected float wallCheckDistance = 0.65f;
    [SerializeField] protected float cliffCheckDistance = 0.4f;
    [SerializeField] protected LayerMask whatIsGround;
    public LayerMask WhatIsGround => whatIsGround;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform ledgeCheck;
    [SerializeField] protected Transform ceilingCheck;
    [SerializeField] protected Transform cliffCheck;

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    public bool IsTouchingLedge => Physics2D.Raycast(ledgeCheck.position, transform.parent.right, wallCheckDistance, whatIsGround);

    public bool IsTouchingWall => Physics2D.Raycast(wallCheck.position, transform.parent.right, wallCheckDistance, whatIsGround);

    public bool IsTouchingWallBack => Physics2D.Raycast(wallCheck.position, -transform.parent.right, wallCheckDistance, whatIsGround);

    public bool IsTouchingCliff => Physics2D.Raycast(cliffCheck.position, Vector2.down, cliffCheckDistance, whatIsGround);

    public bool IsTouchingCeiling => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGroundCheck();
        LoadWallCheck();
        LoadLedgeCheck();
        LoadCeilingCheck();
        LoadCliffCheck();
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

    protected void LoadCeilingCheck()
    {
        if (ceilingCheck != null) return;
        this.ceilingCheck = transform.Find("CeilingCheck");
        Debug.Log(transform.name + " LoadCeilingCheck", gameObject);
    }

    protected void LoadCliffCheck()
    {
        if (cliffCheck != null) return;
        this.cliffCheck = transform.Find("CliffCheck");
        Debug.Log(transform.name + " LoadCliffCheck", gameObject);
    }


    public Vector2 DetermineLedgePos(Vector2 workSpace, int facingDirection)
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        float xDist = xHit.distance;
        workSpace.Set((xDist + 0.015f) * facingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, whatIsGround);
        float yDist = yHit.distance;

        workSpace.Set(wallCheck.position.x + (xDist * facingDirection), ledgeCheck.position.y - yDist);
        return workSpace;
    }

    //protected void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

    //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

    //    Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));

    //    Gizmos.DrawWireSphere(ceilingCheck.position, groundCheckRadius);

    //    Gizmos.DrawLine(cliffCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * cliffCheckDistance));
    //}
}
