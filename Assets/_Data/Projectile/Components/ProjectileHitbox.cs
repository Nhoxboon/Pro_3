using UnityEngine;
using UnityEngine.Events;

public class ProjectileHitbox : ProjectileComponent
{
    public UnityEvent<RaycastHit2D[]> OnRaycastHit2D;

    [field: SerializeField] public Rect HitBoxRect { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }

    private Transform _transform;
    private float checkDistance;

    private RaycastHit2D[] hits;

    private void CheckHitBox()
    {
        hits = Physics2D.BoxCastAll(transform.parent.TransformPoint(HitBoxRect.center), HitBoxRect.size,
            _transform.rotation.eulerAngles.z, _transform.right, checkDistance, LayerMask);

        if (hits.Length <= 0) return;

        OnRaycastHit2D?.Invoke(hits);
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();

        // Just caching the transform based on repeated use (Recommendation from Rider IDE)
        _transform = transform.parent;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Used to compensate for projectile velocity to help stop clipping
        checkDistance = rb.velocity.magnitude * Time.deltaTime;

        CheckHitBox();
    }

    private void OnDrawGizmosSelected()
    {
        // The following is some code that ChatGPT Generated for me to visualize the HitBoxRect based on the rotation.
        // Set up gizmo color
        Gizmos.color = Color.red;

        // Create a new matrix that applies the projectile's rotation
        var rotationMatrix = Matrix4x4.TRS(transform.parent.position,
            Quaternion.Euler(0, 0, transform.parent.rotation.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rotationMatrix;

        // Draw the wireframe cube
        Gizmos.DrawWireCube(HitBoxRect.center, HitBoxRect.size);
    }

    #endregion
}