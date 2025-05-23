using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] protected string[] deathParticles = { "DeathBloodParticles", "DeathChunkParticles" };

    public void Die()
    {
        foreach(string particle in deathParticles)
        {
            core.ParticleManager.StartParticles(particle);
        }
    }
}
