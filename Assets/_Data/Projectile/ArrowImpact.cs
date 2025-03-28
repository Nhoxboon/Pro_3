using UnityEngine;
using Nhoxboon.Projectile;

public class ArrowImpact : NhoxBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Projectile projectile;

    private void FixedUpdate()
    {
        CheckImpact();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadProjectile();
        LoadPlayerLayer();
    }

    private void LoadProjectile()
    {
        if (projectile != null) return;
        projectile = transform.parent.GetComponent<Projectile>();
        Debug.Log(transform.name + " :LoadProjectile", gameObject);
    }

    protected void LoadPlayerLayer()
    {
        if (whatIsPlayer != 0) return;
        whatIsPlayer = LayerMask.GetMask("Player", "Damageable");
        Debug.Log(transform.name + " :LoadPlayerLayer", gameObject);
    }

    public void CheckImpact()
    {
        if (projectile.HasHitGround) return;

        Collider2D damageHit = Physics2D.OverlapCircle(projectile.DamagePosition.position, projectile.DamageRadius, whatIsPlayer);

        if (damageHit)
        {
            Collider2D[] hits = { damageHit };
            projectile.DamageSender.HandleDetectCol2D(hits);
            ProjectileSpawner.Instance.ReturnToPool(projectile.gameObject);
        }
    }
}