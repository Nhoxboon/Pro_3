using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nhoxboon.Projectile
{
    public class Projectile : NhoxBehaviour
    {
        [SerializeField] protected float speed = 3f;
        [SerializeField] protected float travelDistance;
        [SerializeField] protected float xStartPos;
        [SerializeField] protected bool hasHitGround;
        public bool HasHitGround => hasHitGround;

        [SerializeField] protected bool isGravityOn;
        [SerializeField] protected float gravity = 5f;
        [SerializeField] protected float damageRadius = 0.15f;
        public float DamageRadius => damageRadius;

        [SerializeField] protected Rigidbody2D rb;

        [SerializeField] protected Transform damagePosition;
        public Transform DamagePosition => damagePosition;

        [SerializeField] protected DamageSender damageSender;
        public DamageSender DamageSender => damageSender;

        [SerializeField] protected LayerMask whatIsGround;


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
            if (!hasHitGround)
            {
                if (isGravityOn)
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
            LoadDamageSender();

            LoadGroundLayer();
        }

        protected void LoadRigidbody2d()
        {
            if (this.rb != null) return;
            rb = GetComponent<Rigidbody2D>();
            Debug.Log(transform.name + " :LoadRigidbody2d", gameObject);
        }

        protected void LoadDamageSender()
        {
            if (this.damageSender != null) return;
            this.damageSender = GetComponentInChildren<DamageSender>();
            Debug.Log(transform.name + " :LoadDamageSender", gameObject);
        }

        protected void LoadGroundLayer()
        {
            if (whatIsGround != 0) return;
            whatIsGround = LayerMask.GetMask("Ground");
            Debug.Log(transform.name + " :LoadGroundLayer", gameObject);
        }

        private void Status()
        {
            if (!hasHitGround)
            {
                Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

                if (groundHit)
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

        private void OnEnable()
        {
            ResetProjectile();
        }

        public void ResetProjectile()
        {
            hasHitGround = false;
            isGravityOn = false;
            rb.gravityScale = 0f;
            rb.velocity = transform.right * speed;
            xStartPos = transform.position.x;
        }

        public void FireProjectile(float speed, float travelDistance, float damage)
        {
            this.speed = speed;
            this.travelDistance = travelDistance;
            damageSender.damage = damage;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        }
    }
}