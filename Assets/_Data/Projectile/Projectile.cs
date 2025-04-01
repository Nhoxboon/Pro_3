using System;
using UnityEngine;

public class Projectile : NhoxBehaviour
{
//===Old===
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected DamageSender damageSender;
    [SerializeField] protected ProjectileImpact projectileImpact;
    [SerializeField] protected StickToLayer stickToLayer;
    public StickToLayer StickToLayer => stickToLayer;

    public ProjectileImpact ProjectileImpact => projectileImpact;
    public Rigidbody2D Rb => rb;

    public DamageSender DamageSender => damageSender;

    public override void Reset()
    {
        OnReset?.Invoke();
    }

    //===New===
    // This event is used to notify all projectile components that Init has been called
    public event Action OnInit;
    public event Action OnReset;
    public event Action<ProjectileDataPackage> OnReceiveDataPackage;

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
        LoadRigidbody2D();
        LoadDamageSender();
        LoadProjectileImpact();
        LoadStickToLayer();
    }

    protected void LoadRigidbody2D()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
    }

    protected void LoadDamageSender()
    {
        if (damageSender != null) return;
        damageSender = GetComponentInChildren<DamageSender>();
    }

    protected void LoadProjectileImpact()
    {
        if (projectileImpact != null) return;
        projectileImpact = GetComponentInChildren<ProjectileImpact>();
    }
    
    protected void LoadStickToLayer()
    {
        if (stickToLayer != null) return;
        stickToLayer = GetComponentInChildren<StickToLayer>();
    }

// private void OnEnable()
    // {
    //     ResetProjectile();
    // }


    // public void ResetProjectile()
    // {
    //     hasHitGround = false;
    //     isGravityOn = false;
    //     rb.gravityScale = 0f;
    //     rb.velocity = transform.right * speed;
    //     xStartPos = transform.position.x;
    //     Reset();
    // }

    // public void FireProjectile(float speed, float travelDistance, float damage)
    // {
    //     this.speed = speed;
    //     this.travelDistance = travelDistance;
    //     damageSender.damage = damage;
    // }
}