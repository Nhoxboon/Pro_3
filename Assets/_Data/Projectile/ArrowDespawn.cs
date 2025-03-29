using UnityEngine;

public class ArrowDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        ProjectileSpawner.Instance.Despawn(transform.parent.gameObject);
    }
}