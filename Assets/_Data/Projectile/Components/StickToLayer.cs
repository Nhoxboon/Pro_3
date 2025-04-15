using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class StickToLayer : ProjectileComponent
{
    [SerializeField] public UnityEvent setStuck;
    [SerializeField] public UnityEvent setUnstuck;

    [SerializeField] protected LayerMask layerMask;

   [SerializeField] protected string inactiveSortingLayerName = "InactiveProjectile";
   
   [SerializeField] protected string activeSortingLayerName;
   
   [SerializeField] protected float checkDistance = 2;

    protected SpriteRenderer sr => projectile.sr;
    protected Transform _transform;

    protected float gravityScale;

    [SerializeField] protected bool isStuck;

    protected Vector3 offsetPosition;
    protected Quaternion offsetRotation;

    protected OnDisableNotifier onDisableNotifier;

    protected Transform referenceTransform;
    protected bool subscribedToDisableNotifier;

    protected void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (isStuck)
            return;

        SetStuck();

        // The point returned by the boxcast can be weird, so we do one last check by firing a ray from the origin to the right to find
        // a more suitable resting point
        var lineHit = Physics2D.Raycast(_transform.position, _transform.right, checkDistance, layerMask);
        
        if (lineHit)
        {
            SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
            return;
        }

        foreach (var hit in hits)
        {
            if (!LayerMaskUtilities.IsLayerInMask(hit, layerMask))
                continue;

            SetReferenceTransformAndPoint(hit.transform, hit.point);
            return;
        }

        SetUnstuck();
    }

    // Set projectile position to point and set new reference transform for projectile to track
    protected void SetReferenceTransformAndPoint(Transform newReferenceTransform, Vector2 newPoint)
    {
        if (newReferenceTransform.TryGetComponent(out onDisableNotifier))
        {
            onDisableNotifier.OnDisableEvent += HandleDisableNotifier;
            subscribedToDisableNotifier = true;
        }

        // Set projectile position to detected point
        _transform.position = newPoint;

        referenceTransform = newReferenceTransform;
        offsetPosition = _transform.position - referenceTransform.position;
        offsetRotation = Quaternion.Inverse(referenceTransform.rotation) * _transform.rotation;
    }
    
    protected void SetStuck()
    {
        isStuck = true;

        sr.sortingLayerName = inactiveSortingLayerName;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        setStuck?.Invoke();
    }
    
    protected void SetUnstuck()
    {
        isStuck = false;

        sr.sortingLayerName = activeSortingLayerName;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;

        setUnstuck?.Invoke();
    }
    
    protected void HandleDisableNotifier()
    {
        SetUnstuck();

        if (!subscribedToDisableNotifier)
            return;

        onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
        subscribedToDisableNotifier = false;
    }

    protected override void ResetProjectile()
    {
        base.ResetProjectile();

        SetUnstuck();
    }
    
    protected void UpdateStuckPositionAndRotation()
    {
        var referenceRotation = referenceTransform.rotation;
        _transform.position = referenceTransform.position + referenceRotation * offsetPosition;
        _transform.rotation = referenceRotation * offsetRotation;
    }
    
    #region Plumbing

    protected override void Awake()
    {
        base.Awake();

        gravityScale = rb.gravityScale;

        _transform = transform.parent;

        activeSortingLayerName = sr.sortingLayerName;


        projectile.ProjectileHitbox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
    }

    protected override void Update()
    {
        base.Update();

        if (!isStuck)
            return;

        if (!referenceTransform)
        {
            SetUnstuck();
            return;
        }

        UpdateStuckPositionAndRotation();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        projectile.ProjectileHitbox.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);

        if (subscribedToDisableNotifier) onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
    }

    #endregion
}