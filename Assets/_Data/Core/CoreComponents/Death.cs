using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] protected string[] deathParticles = { "DeathBloodParticles", "DeathChunkParticles" };

    private void OnEnable()
    {
        core.Stats.Health.OnCurrentValueZero += Die;
    }

    private void OnDisable()
    {
        core.Stats.Health.OnCurrentValueZero -= Die;
    }

    public void Die()
    {
        foreach(string particle in deathParticles)
        {
            core.ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }
}
