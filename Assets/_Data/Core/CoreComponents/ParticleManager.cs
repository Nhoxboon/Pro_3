using UnityEngine;

public class ParticleManager : CoreComponent
{
    public GameObject StartParticles(string particleName, Vector3 position, Quaternion rotation)
    {
        var particle = ParticleSpawner.Instance.Spawn(particleName, position, rotation);
        particle.gameObject.SetActive(true);
        return particle != null ? particle.gameObject : null;
    }

    public GameObject StartParticles(string particleName)
    {
        return StartParticles(particleName, transform.position, Quaternion.identity);
    }

    public GameObject StartParticlesWithRandomRotation(string particleName)
    {
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        return StartParticles(particleName, transform.position, randomRotation);
    }

    public GameObject StartWithRandomRotation(string particleName, Vector2 offset)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particleName, FindRelativePoint(offset), randomRotation);
    }

    public GameObject StartParticlesRelative(string particleName, Vector2 offset, Quaternion rotation)
    {
        var pos = FindRelativePoint(offset);

        return StartParticles(particleName, pos, rotation);
    }

    private Vector2 FindRelativePoint(Vector2 offset)
    {
        return core.Movement.FindRelativePoint(offset);
    }
}