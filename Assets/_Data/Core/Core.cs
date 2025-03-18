using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : NhoxBehaviour
{
    [SerializeField]protected Movement movement;
    public Movement Movement => movement;

    [SerializeField] protected TouchingDirection touchingDirection;
    public TouchingDirection TouchingDirection => touchingDirection;

    [SerializeField] protected Combat combat;
    public Combat Combat => combat;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
        LoadTouchingDirection();
        LoadCombat();
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

    protected void LoadCombat()
    {
        if (combat != null) return;
        combat = GetComponentInChildren<Combat>();
        Debug.Log(transform.name + " LoadCombat", gameObject);
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
        combat.LogicUpdate();
    }
}
