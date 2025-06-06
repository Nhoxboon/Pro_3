public class DamageReceiver : CoreComponent
{
    protected string damageParticle = "HitParticles_1";

    //TODO: Need to process this
    /*
     * Modifiers allows us to perform some custom logic on our DamageData before we apply it here. An example where this is being used is by the Block weapon component.
     * Blocking works by assigning a modifier during the active block window of the shield that reduces the amount of damage the player will take. For example: If a shield
     * has a damage absorption property of 0.75 and we deal 10 damage, only 2.5 will actually end up getting removed from player stats after applying the modifier.
     */
    public Modifiers<Modifier<CombatDamageData>, CombatDamageData> Modifiers { get; } = new();

    public virtual void Damage(CombatDamageData data)
    {
        print($"Damage Amount Before Modifiers: {data.Amount}");

        // We must apply the modifiers before we do anything else with data. If there are no modifiers currently active, data will remain the same
        data = Modifiers.ApplyAllModifiers(data);

        print($"Damage Amount After Modifiers: {data.Amount}");

        if (data.Amount <= 0f) return;

        core.Stats.Health.Decrease(data.Amount);
        core.ParticleManager.StartParticlesWithRandomRotation(damageParticle);
    }
}