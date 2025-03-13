using UnityEngine;

public class ArrowDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        ProjectileSpawner.Instance.ReturnToPool(transform.parent.gameObject);
    }
}