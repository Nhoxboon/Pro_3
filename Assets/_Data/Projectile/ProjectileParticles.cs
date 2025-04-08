
using UnityEngine;

public class ProjectileParticles : NhoxBehaviour
{
    [SerializeField] private string impactParticles = "MagicMissleImpactParticle";
    
    public void SpawnImpactParticles(Vector3 position, Quaternion rotation)
    {
        Transform impactParticle = ParticleSpawner.Instance.Spawn(impactParticles, position, rotation);
        impactParticle.gameObject.SetActive(true);
    }
    
    public void SpawnImpactParticles(RaycastHit2D hit)
    {
        Quaternion rotation = Quaternion.FromToRotation(transform.parent.right, hit.normal);
        
        SpawnImpactParticles(hit.point, rotation);
    }
    
    public void SpawnImpactParticles(RaycastHit2D[] hits)
    {
        if(hits.Length <= 0 ) return;
        
        SpawnImpactParticles(hits[0]);
    }

}
