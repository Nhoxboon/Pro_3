using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : NhoxBehaviour
{
    [SerializeField] protected float groundCheckRadius;
    [SerializeField] protected LayerMask whatIsGround;

    [SerializeField] protected bool isGrounded;
    public bool IsGrounded => isGrounded;

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    protected void CheckSurroundings()
    {
        this.isGrounded = Physics2D.OverlapCircle(this.transform.position, this.groundCheckRadius, this.whatIsGround);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.groundCheckRadius);
    }
}
