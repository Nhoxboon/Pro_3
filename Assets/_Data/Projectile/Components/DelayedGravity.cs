using UnityEngine;

public class DelayedGravity : ProjectileComponent
{
    [field: SerializeField] public float Distance { get; private set; } = 5f;

    [SerializeField] protected float gravity = 4f;

    // Used so other projectile components, such as DrawModifyDelayedGravity, can modify how far the projectile travels before being affected by gravity
    [SerializeField] public float distanceMultiplier = 1;

    protected DistanceNotifier distanceNotifier = new();

    private void HandleNotify()
    {
        rb.gravityScale = gravity;
    }

    // On Init, enable the distance notifier to trigger once distance has been travelled.
    protected override void Init()
    {
        base.Init();

        rb.gravityScale = 0f;
        distanceNotifier.Init(transform.parent.position, Distance * distanceMultiplier);
        distanceMultiplier = 1;
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();

        gravity = rb.gravityScale;

        distanceNotifier.OnNotify += HandleNotify;
    }

    protected override void Update()
    {
        base.Update();

        distanceNotifier?.Tick(transform.position);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        distanceNotifier.OnNotify -= HandleNotify;
    }

    #endregion
}