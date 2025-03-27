using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DamageReceiver : CoreComponent
{
    protected string damageParticle = "HitParticles";

    public virtual void Damage(float amount)
    {
        core.Stats.Health.Decrease(amount);
        core.ParticleManager.StartParticlesWithRandomRotation(damageParticle);
    }
}
