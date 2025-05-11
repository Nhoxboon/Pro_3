
using System.Collections;
using UnityEngine;

public class ProjectileDespawnByChoice : ProjectileComponent
{
    protected override void Awake()
    {
        base.Awake();
        projectile.OnDespawn += HandleDespawn;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        projectile.OnDespawn -= HandleDespawn;
    }

    public void DespawnObject()
    {
        ProjectileSpawner.Instance.Despawn(transform.parent.gameObject);
    }
    
    protected void HandleDespawn(float despawnTime)
    {
        StartCoroutine(DespawnAfterDelay(despawnTime));
    }

    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DespawnObject();
    }

}
