using UnityEngine;

public class ProjectileMovement : ProjectileComponent
{
    [SerializeField] protected bool applyContinuously;
    [field: SerializeField] public float Speed { get; private set; }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!applyContinuously)
            return;

        SetVelocity();
    }

    protected override void Init()
    {
        base.Init();

        SetVelocity();
    }

    private void SetVelocity()
    {
        rb.velocity = Speed * transform.parent.right;
    }
}