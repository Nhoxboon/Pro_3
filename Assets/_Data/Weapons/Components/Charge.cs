using UnityEngine;

public class Charge : WeaponComponent<ChargeData, AttackCharge>
{
    protected int currentCharge;

    protected TimeNotifier timeNotifier;

    public int TakeFinalChargeReading()
    {
        timeNotifier.Disable();
        return currentCharge;
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();

        currentCharge = currentAttackData.initialChargeAmount;

        timeNotifier.Init(currentAttackData.chargeTime, true);
    }

    protected override void HandleExit()
    {
        base.HandleExit();
        timeNotifier.Disable();
    }

    protected void HandleNotify()
    {
        AudioManager.Instance.PlaySFX(currentAttackData.chargeSound);
        currentCharge++;

        if (currentCharge >= currentAttackData.numberOfCharges)
        {
            currentCharge = currentAttackData.numberOfCharges;
            timeNotifier.Disable();

            Core.ParticleManager.StartParticlesRelative(currentAttackData.fullyChargedIndicatorParticleName,
                currentAttackData.particlesOffset, Quaternion.identity);
        }

        else
        {
            Core.ParticleManager.StartParticlesRelative(currentAttackData.chargeIncreaseIndicatorParticalName,
                currentAttackData.particlesOffset, Quaternion.identity);
        }
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();

        timeNotifier = new TimeNotifier();

        timeNotifier.OnNotify += HandleNotify;
    }

    private void Update()
    {
        timeNotifier.Tick();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        timeNotifier.OnNotify -= HandleNotify;
    }

    #endregion
}