using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : NhoxBehaviour
{
    [SerializeField]protected Movement movement;
    public Movement Movement => movement;

    [SerializeField] protected TouchingDirection touchingDirection;
    public TouchingDirection TouchingDirection => touchingDirection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
        LoadTouchingDirection();
    }

    protected void LoadMovement()
    {
        if (movement != null) return;
        movement = GetComponentInChildren<Movement>();
        Debug.Log(transform.name + " LoadMovement", gameObject);
    }

    protected void LoadTouchingDirection()
    {
        if (touchingDirection != null) return;
        touchingDirection = GetComponentInChildren<TouchingDirection>();
        Debug.Log(transform.name + " LoadTouchingDirection", gameObject);
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
    }
}
