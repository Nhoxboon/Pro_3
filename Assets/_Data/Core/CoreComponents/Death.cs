using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] protected string[] deathParticles = { "DeathBloodParticles", "DeathChunkParticles" };

    private void OnEnable()
    {
        core.Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        core.Stats.OnHealthZero -= Die;
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
