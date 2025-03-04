using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NhoxBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 9f;
    [SerializeField] protected float wallSlideSpeed = 1f;
    [SerializeField] protected float airDragMultiplier = 0.95f;
    [SerializeField] protected int facingDirection = 1;

    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 20f;
    [SerializeField] protected int amountOfJumpsLeft;
    [SerializeField] protected int amountOfJumps = 1;
    [SerializeField] protected float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] protected bool checkJumpMultiplier;
    [SerializeField] protected bool canNormalJump;
    [SerializeField] protected float jumpTimer;
    [SerializeField] protected float jumpTimerSet = 0.15f;
    [SerializeField] protected bool isAttemptingToJump;
    [SerializeField] protected float turnTimer;
    [SerializeField] protected float turnTimerSet = 0.1f;

    [Header("Wall jump")]
    [SerializeField] protected bool canWallJump;
    [SerializeField] protected Vector2 wallJumpDirection = new Vector2(1, 2);
    [SerializeField] protected float wallJumpForce = 30f;
    [SerializeField] protected float wallJumpTimer;
    [SerializeField] protected float wallJumpTimerSet = 0.5f;
    [SerializeField] protected bool hasWallJumped;
    [SerializeField] protected int lastWallJumpDirection;

    [Header("Movement Variables")]
    [SerializeField] protected float movementInputDirection;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected bool canFlip;
    [SerializeField] protected bool canMove;
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
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckFlip();
        CheckCanJump();
        CheckIfWallSliding();
        JumpState();
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
            if(PlayerCtrl.Instance.TouchingDirection.IsGrounded || (amountOfJumpsLeft > 0 && !PlayerCtrl.Instance.TouchingDirection.IsTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if(InputManager.Instance.HorizontalButtonPressed && PlayerCtrl.Instance.TouchingDirection.IsTouchingWall)
        {
            if(!PlayerCtrl.Instance.TouchingDirection.IsGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if(!canMove)
        {
            turnTimer -= Time.deltaTime;
            if(turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if(checkJumpMultiplier && !InputManager.Instance.JumpHeld)
        {
            checkJumpMultiplier = false;
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
        if(!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.parent.Rotate(0, 180, 0);
        }
    }

    protected void CheckCanJump()
    {
        if (PlayerCtrl.Instance.TouchingDirection.IsGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if(PlayerCtrl.Instance.TouchingDirection.IsTouchingWall)
        {
            checkJumpMultiplier = false;
            canWallJump = true;
        }
        else
        {
            canWallJump = false;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    protected void CheckIfWallSliding()
    {
        if(PlayerCtrl.Instance.TouchingDirection.IsTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
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
        if (!PlayerCtrl.Instance.TouchingDirection.IsGrounded && !isWallSliding && movementInputDirection == 0)
        {
            ApplyAirDrag();
        }
        else if(canMove)
        {
            ApplyGroundMovement();
        }

        WallSlide();
    }

    protected void ApplyGroundMovement()
    {
        rb.velocity = new Vector2(movementInputDirection * moveSpeed, rb.velocity.y);
    }

    protected void ApplyAirDrag()
    {
        rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
    }


    protected void WallSlide()
    {
        if (isWallSliding)
        {
            amountOfJumpsLeft = amountOfJumps;
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    protected void JumpState()
    {
        if(jumpTimer > 0)
        {
            if(!PlayerCtrl.Instance.TouchingDirection.IsGrounded && PlayerCtrl.Instance.TouchingDirection.IsTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if(PlayerCtrl.Instance.TouchingDirection.IsGrounded || PlayerCtrl.Instance.TouchingDirection.IsTouchingWall)
            {
                NormalJump();
            }
        }
        if(isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
        if(wallJumpTimer > 0)
        {
            if(hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                hasWallJumped = false;
            }
            
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
        else if (wallJumpTimer <= 0)
        {
            hasWallJumped = false;
        }
    }

    protected void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    protected void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }
}
