using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : CoreComponent
{
    public GameObject StartParticles(string particleName, Vector3 position, Quaternion rotation)
    {
        Transform particle = ParticleSpawner.Instance.Spawn(particleName, position, rotation);
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
}
