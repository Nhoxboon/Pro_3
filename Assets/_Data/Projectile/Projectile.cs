using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : NhoxBehaviour
{
//===Old===
    public SpriteRenderer sr;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected DamageSender damageSender;
    [FormerlySerializedAs("projectileImpact")] [SerializeField] protected ProjectileHitbox projectileHitbox;
    [SerializeField] protected StickToLayer stickToLayer;
    public StickToLayer StickToLayer => stickToLayer;

    public ProjectileHitbox ProjectileHitbox => projectileHitbox;
    public Rigidbody2D Rb => rb;

    public DamageSender DamageSender => damageSender;

    public override void Reset()
    {
        base.Reset();
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
        LoadSpriteRenderer();
        LoadRigidbody2D();
        LoadDamageSender();
        LoadProjectileImpact();
        LoadStickToLayer();
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

    protected void LoadProjectileImpact()
    {
        if (projectileHitbox != null) return;
        projectileHitbox = GetComponentInChildren<ProjectileHitbox>();
    }
    
    protected void LoadStickToLayer()
    {
        if (stickToLayer != null) return;
        stickToLayer = GetComponentInChildren<StickToLayer>();
    }

    // public void FireProjectile(float speed, float travelDistance, float damage)
    // {
    //     this.speed = speed;
    //     this.travelDistance = travelDistance;
    //     damageSender.damage = damage;
    // }
}