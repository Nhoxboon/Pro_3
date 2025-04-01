using UnityEngine;

public class ProjectileMovement : ProjectileComponent
{
    [SerializeField] protected bool applyContinuously;
    [field: SerializeField] public float Speed { get; private set; }

    // On Init, set projectile velocity once
    protected override void Init()
    {
        base.Init();

        SetVelocity();
    }

    private void SetVelocity() => rb.velocity = Speed * transform.parent.right;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
            
        if (!applyContinuously)
            return;
            
        SetVelocity();
    }
}
