using System;
using UnityEngine;

public class Projectile : NhoxBehaviour
{
    public SpriteRenderer sr;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected DamageSender damageSender;
    [SerializeField] protected ProjectileHitbox projectileHitbox;

    public ProjectileHitbox ProjectileHitbox => projectileHitbox;
    public Rigidbody2D Rb => rb;
    
    // This event is used to notify all projectile components that Init has been called
    public event Action OnInit;
    public event Action OnReset;
    public event Action<ProjectileDataPackage> OnReceiveDataPackage;

    public override void Reset()
    {
        base.Reset();
        OnReset?.Invoke();
    }

    public void Init()
    {
        OnInit?.Invoke();
    }

    public void SendDataPackage(ProjectileDataPackage dataPackage)
    {
        OnReceiveDataPackage?.Invoke(dataPackage);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
        LoadRigidbody2D();
        LoadDamageSender();
        LoadProjectileHitbox();
    }

    protected void LoadSpriteRenderer()
    {
        if (sr != null) return;
        sr = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(transform.name + "LoadSpriteRenderer", gameObject);
    }

    protected void LoadRigidbody2D()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + "LoadRigidbody2D", gameObject);
    }

    protected void LoadDamageSender()
    {
        if (damageSender != null) return;
        damageSender = GetComponentInChildren<DamageSender>();
    }

    protected void LoadProjectileHitbox()
    {
        if (projectileHitbox != null) return;
        projectileHitbox = GetComponentInChildren<ProjectileHitbox>();
    }
}