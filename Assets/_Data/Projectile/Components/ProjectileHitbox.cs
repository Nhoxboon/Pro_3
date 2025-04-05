using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ProjectileHitbox : ProjectileComponent
{
    public UnityEvent<RaycastHit2D[]> OnRaycastHit2D;

    [SerializeField] public Rect hitBoxRect;
    [SerializeField] public LayerMask layerMask;
    
    private float checkDistance;

    private RaycastHit2D[] hits;

    private void CheckHitBox()
    {
        hits = Physics2D.BoxCastAll(transform.parent.TransformPoint(hitBoxRect.center), hitBoxRect.size,
            transform.parent.rotation.eulerAngles.z, transform.parent.right, checkDistance, layerMask);

        if (hits.Length <= 0) return;

        OnRaycastHit2D?.Invoke(hits);
    }
    
    protected void CheckDistance()
    {
        // Used to compensate for projectile velocity to help stop clipping
        checkDistance = rb.velocity.magnitude * Time.deltaTime;
    }

    #region Plumbing

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckDistance();
        CheckHitBox();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadLayerMask();
    }
    
    protected void LoadLayerMask()
    {
        if (layerMask != 0) return;
        layerMask = LayerMask.GetMask("Ground", "Damageable");
        Debug.Log(transform.name + ": LoadLayerMask", gameObject);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        var rotationMatrix = Matrix4x4.TRS(transform.parent.position,
            Quaternion.Euler(0, 0, transform.parent.rotation.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(hitBoxRect.center, hitBoxRect.size);
    }

    #endregion
}