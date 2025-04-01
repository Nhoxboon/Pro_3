using UnityEngine;

public class RotateTowardsVelocity : ProjectileComponent
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var velocity = rb.velocity;

        if (velocity.Equals(Vector3.zero))
            return;

        // Find velocity vector angle
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        // Apply angle as rotation around Vector3.forward (So using the vector pointing in to your screen as the axis around which we are rotating)
        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}