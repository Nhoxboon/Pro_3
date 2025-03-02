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
    [SerializeField] protected float wallCheckDistance = 0.4f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGroundCheck();
        LoadWallCheck();
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

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    protected void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.parent.right, wallCheckDistance, whatIsGround);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
