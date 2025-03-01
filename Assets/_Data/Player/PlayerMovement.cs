using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NhoxBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 16f;
    [SerializeField] protected int amountOfJumpsLeft;
    [SerializeField] protected int amountOfJumps = 1;
    [SerializeField] protected bool canJump;

    [Header("Movement Variables")]
    [SerializeField] protected float movementInputDirection;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected bool isMoving;
    public bool IsMoving => isMoving;

    [Header("Components")]
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    protected override void Start()
    {
        base.Start();
        amountOfJumpsLeft = amountOfJumps;
    }

    private void Update()
    {
        this.CheckInput();
        this.CheckFlip();
        this.CheckCanJump();
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
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2d", gameObject);
    }

    protected void CheckInput()
    {
        movementInputDirection = InputManager.Instance.HorizontalInput;

        if(InputManager.Instance.JumpPressed)
        {
            Jump();
        }
    }

    protected void CheckFlip()
    {
        if (movementInputDirection > 0 && !isFacingRight || movementInputDirection < 0 && isFacingRight)
        {
            Flip();
        }

        if(rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.parent.Rotate(0f, 180f, 0f);
    }

    protected void CheckCanJump()
    {
        if (PlayerCtrl.Instance.TouchingDirection.IsGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }


    protected void ApplyMovement()
    {
        rb.velocity = new Vector2(movementInputDirection * moveSpeed, rb.velocity.y);
    }

    protected void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

}
