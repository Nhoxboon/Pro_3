using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NhoxBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float wallSlideSpeed = 2f;
    [SerializeField] protected float movementForceInAir = 50f;
    [SerializeField] protected float airDragMultiplier = 0.95f;

    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 16f;
    [SerializeField] protected int amountOfJumpsLeft;
    [SerializeField] protected int amountOfJumps = 1;
    [SerializeField] protected float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] protected bool canJump;

    [Header("Movement Variables")]
    [SerializeField] protected float movementInputDirection;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected bool isMoving;
    public bool IsMoving => isMoving;
    [SerializeField] protected bool isWallSliding;
    public bool IsWallSliding => isWallSliding;

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
        CheckInput();
        CheckFlip();
        CheckCanJump();
        CheckIfWallSliding();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody2d();
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

        if(InputManager.Instance.JumpReleased)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
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
        if(!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.parent.localScale;
            scale.x *= -1;
            transform.parent.localScale = scale;
        }
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

    protected void CheckIfWallSliding()
    {
        if(PlayerCtrl.Instance.TouchingDirection.IsTouchingWall && !PlayerCtrl.Instance.TouchingDirection.IsGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    protected void ApplyMovement()
    {
        if (PlayerCtrl.Instance.TouchingDirection.IsGrounded)
        {
            ApplyGroundMovement();
        }
        else if (!PlayerCtrl.Instance.TouchingDirection.IsGrounded && !isWallSliding && movementInputDirection != 0)
        {
            ApplyAirMovementWithInput();
        }
        else if (!PlayerCtrl.Instance.TouchingDirection.IsGrounded && !isWallSliding && movementInputDirection == 0)
        {
            ApplyAirDrag();
        }

        WallSlide();
    }

    protected void ApplyGroundMovement()
    {
        rb.velocity = new Vector2(movementInputDirection * moveSpeed, rb.velocity.y);
    }

    protected void ApplyAirMovementWithInput()
    {
        Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
        rb.AddForce(forceToAdd);

        if (Mathf.Abs(rb.velocity.x) > moveSpeed)
        {
            rb.velocity = new Vector2(moveSpeed * movementInputDirection, rb.velocity.y);
        }
    }

    protected void ApplyAirDrag()
    {
        rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
    }


    protected void WallSlide()
    {
        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
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
