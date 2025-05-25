using System;
using UnityEngine;

public class Movement : CoreComponent
{
    public event Action OnFlip;
    
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected int facingDirection;

    public bool canSetVelocity;

    [SerializeField] protected Vector2 workSpace;
    [SerializeField] protected Vector2 currentVelocity;
    public Rigidbody2D Rb => rb;
    public int FacingDirection => facingDirection;
    public Vector2 CurrentVelocity => currentVelocity;

    protected override void Awake()
    {
        base.Awake();
        facingDirection = 1;
        canSetVelocity = true;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody2D();
    }

    protected void LoadRigidbody2D()
    {
        if (rb != null) return;
        rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2D", gameObject);
    }

    public override void LogicUpdate()
    {
        currentVelocity = rb.velocity;
    }

    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(currentVelocity.x, velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityZero()
    {
        workSpace = Vector2.zero;
        SetFinalVelocity();
    }

    protected void SetFinalVelocity()
    {
        if (canSetVelocity)
        {
            rb.velocity = workSpace;
            currentVelocity = workSpace;
        }
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection) Flip();
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.parent.parent.Rotate(0, 180, 0);
        OnFlip?.Invoke();
    }

    public void SetFacingDirection(int dir)
    {
        facingDirection = dir;
    }

    public Vector2 FindRelativePoint(Vector2 offset)
    {
        offset.x *= facingDirection;

        //Note: maybe use transform.parent.parent instead
        return transform.position + (Vector3)offset;
    }
}