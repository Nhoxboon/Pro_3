
using System;
using UnityEngine;

public class ParryReceiver : CoreComponent
{
    public event Action OnParried;
        
    [SerializeField] protected Collider2D parryCollider;

    public void Parry(CombatParryData data)
    {
        OnParried?.Invoke();
    }
        
    public void SetParryColliderActive(bool value)
    {
        parryCollider.enabled = value;
    }
        
    protected override void Awake()
    {
        base.Awake();

        parryCollider.enabled = false;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParryCollider();
    }

    protected void LoadParryCollider()
    {
        if(parryCollider != null) return;
        parryCollider = GetComponent<Collider2D>();
        Debug.Log(transform.name + " :LoadParryCollider", gameObject);
    }
}
