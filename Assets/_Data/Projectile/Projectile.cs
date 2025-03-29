using System;
using UnityEngine;

namespace Nhoxboon.Projectile
{
    public class Projectile : NhoxBehaviour
    {
        // === Các thuộc tính từ hệ thống mới === //
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

        // === Sự kiện từ hệ thống cũ === //
        public event Action OnInit;
        public event Action OnReset;
        public event Action<ProjectileDataPackage> OnReceiveDataPackage;

        protected override void Start()
        {
            base.Start();
            Init(); 
        }

        public void Init()
        {
            rb.gravityScale = 0f;
            rb.velocity = transform.right * speed;
            isGravityOn = false;
            xStartPos = transform.position.x;
            OnInit?.Invoke();
        }

        // Phương thức Reset từ hệ thống cũ
        public override void Reset()
        {
            base.Reset();
            OnReset?.Invoke();
        }
        
        public void SendData(ProjectileDataPackage spawnInfo)
        {
            OnReceiveDataPackage?.Invoke(spawnInfo);
        }

        // === Logic từ hệ thống mới === //
        private void Update()
        {
            if (!hasHitGround && isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void FixedUpdate() => Status();

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadRigidbody2D();
            LoadDamageSender();
            LoadGroundLayer();
        }

        private void LoadRigidbody2D()
        {
            if (rb != null) return;
            rb = GetComponent<Rigidbody2D>();
        }

        private void LoadDamageSender()
        {
            if (damageSender != null) return;
            damageSender = GetComponentInChildren<DamageSender>();
        }

        private void LoadGroundLayer()
        {
            if (whatIsGround != 0) return;
            whatIsGround = LayerMask.GetMask("Ground");
        }

        private void Status()
        {
            if (hasHitGround) return;

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

        private void OnEnable() => ResetProjectile();

        public void ResetProjectile()
        {
            hasHitGround = false;
            isGravityOn = false;
            rb.gravityScale = 0f;
            rb.velocity = transform.right * speed;
            xStartPos = transform.position.x;
            Reset(); 
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