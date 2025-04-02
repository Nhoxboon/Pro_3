using System.Collections;
using UnityEngine;

public class ProjectileDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        ProjectileSpawner.Instance.Despawn(transform.parent.gameObject);
    }
    
    public void DespawnObject(float delay)
    {
        StartCoroutine(ReturnItemWithDelay(delay));
    }
    
    private IEnumerator ReturnItemWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        DespawnObject();
    }
}