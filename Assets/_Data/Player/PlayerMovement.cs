using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NhoxBehaviour
{
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float jumpForce = 16f;
    [SerializeField] protected float movementInputDirection;
    [SerializeField] protected bool isFacingRight = true;

    [SerializeField] protected Rigidbody2D rb;

    private void Update()
    {
        this.CheckInput();
        this.CheckFlip();
    }

    private void FixedUpdate()
    {
        this.ApplyMovement();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2d();
    }

    protected void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        this.rb = this.GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2d", gameObject);
    }

    protected void CheckInput()
    {
        this.movementInputDirection = InputManager.Instance.HorizontalInput;

        if(InputManager.Instance.JumpPressed)
        {
            this.Jump();
        }
    }

    protected void CheckFlip()
    {
        if (this.movementInputDirection > 0 && !this.isFacingRight || this.movementInputDirection < 0 && this.isFacingRight)
        {
            this.Flip();
        }
    }

    protected void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        this.transform.parent.Rotate(0f, 180f, 0f);
    }

    protected void ApplyMovement()
    {
        this.rb.velocity = new Vector2(this.movementInputDirection * moveSpeed, this.rb.velocity.y);
    }

    protected void Jump()
    {
        this.rb.velocity = new Vector2(this.rb.velocity.x, jumpForce);
    }
}
