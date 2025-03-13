using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : NhoxBehaviour
{
    [SerializeField] protected AttackDetails attackDetails;
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float travelDistance;
    [SerializeField] protected float xStartPos;
    [SerializeField] protected bool hasHitGround;
    [SerializeField] protected bool isGravityOn;
    [SerializeField] protected float gravity = 5f;
    [SerializeField] protected float damageRadius = 0.15f;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;

    [SerializeField] protected Transform damagePosition;

    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;
        rb.velocity = transform.right * speed;
        isGravityOn = false;
        xStartPos = transform.position.x;
    }

    private void Update()
    {
        if(!hasHitGround)
        {
            attackDetails.position = transform.position;

            if(isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        Status();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody2d();
    }

    protected void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " :LoadRigidbody2d", gameObject);
    }

    protected void Status()
    {
        if(!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if(damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }
            if(groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
