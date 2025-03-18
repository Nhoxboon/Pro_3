using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : NhoxBehaviour
{
    [SerializeField]protected Movement movement;
    public Movement Movement => movement;

    [SerializeField] protected TouchingDirection touchingDirection;
    public TouchingDirection TouchingDirection => touchingDirection;

    [SerializeField] protected Stats stats;
    public Stats Stats => stats;

    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver => damageReceiver;

    [SerializeField] protected Knockbackable knockbackable;
    public Knockbackable Knockbackable => knockbackable;

    [SerializeField] protected List<CoreComponent> components = new List<CoreComponent>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
        LoadTouchingDirection();
        LoadStats();

        LoadDamageReceiver();
        LoadKnockbackable();
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

    protected void LoadStats()
    {
        if (stats != null) return;
        stats = GetComponentInChildren<Stats>();
        Debug.Log(transform.name + " LoadStats", gameObject);
    }

    protected void LoadDamageReceiver()
    {
        if (damageReceiver != null) return;
        damageReceiver = GetComponentInChildren<DamageReceiver>();
        Debug.Log(transform.name + " LoadDamageReceiver", gameObject);
    }

    protected void LoadKnockbackable()
    {
        if (knockbackable != null) return;
        knockbackable = GetComponentInChildren<Knockbackable>();
        Debug.Log(transform.name + " LoadKnockbackable", gameObject);
    }

    public void LogicUpdate()
    {
        foreach (CoreComponent component in components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }
}
