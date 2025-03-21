using UnityEngine;

public class DespawnForParticleSystem : Despawn
{
    [SerializeField] protected new ParticleSystem particleSystem;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParticleSystem();
    }

    protected virtual void LoadParticleSystem()
    {
        if (particleSystem != null) return;
        particleSystem = transform.parent.GetComponent<ParticleSystem>();
        Debug.Log(transform.name + " :LoadParticleSystem", gameObject);
    }

    protected override bool CanDespawn()
    {
        return particleSystem != null && particleSystem.isStopped;
    }

    public override void DespawnObject()
    {
        ParticleSpawner.Instance.ReturnToPool(transform.parent.gameObject);
    }
}