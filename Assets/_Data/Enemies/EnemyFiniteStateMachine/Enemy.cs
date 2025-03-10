using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NhoxBehaviour
{
    protected FiniteStateMachine stateMachine;
    public FiniteStateMachine StateMachine => stateMachine;

    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    [SerializeField] protected int facingDirection;
    public int FacingDirection => facingDirection;

    [SerializeField] protected Vector2 workSpace;

    protected override void Start()
    {
        base.Start();
        facingDirection = 1;
        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
    }

    protected void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = this.GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + " LoadRigidbody2D", gameObject);
    }

    public virtual void SetVelocityX(float velocity)
    {
        workSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = workSpace;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.parent.Rotate(0.0f, 180.0f, 0.0f);
    }
}
