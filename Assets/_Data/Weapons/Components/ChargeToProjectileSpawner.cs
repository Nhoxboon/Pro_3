public class ChargeToProjectileSpawner : WeaponComponent<ChargeToProjectileSpawnerData, AttackChargeToProjectileSpawner>
{
    protected Charge charge;

    protected bool hasReadCharge;
    protected ProjectileSpawnForWeapon projectileSpawner;

    protected override void HandleEnter()
    {
        base.HandleEnter();

        hasReadCharge = false;
    }

    private void HandleCurrentInputChange(bool newInput)
    {
        if (newInput || hasReadCharge)
            return;

        projectileSpawner.angleVariation = currentAttackData.angleVariation;
        projectileSpawner.chargeAmount = charge.TakeFinalChargeReading();

        hasReadCharge = true;
    }

    #region Plumbing

    protected override void Start()
    {
        base.Start();

        weapon.OnCurrentInputChange += HandleCurrentInputChange;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        weapon.OnCurrentInputChange -= HandleCurrentInputChange;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadProjectileSpawnerForWeapon();
        LoadCharge();
    }

    protected void LoadProjectileSpawnerForWeapon()
    {
        if (projectileSpawner != null) return;
        projectileSpawner = GetComponent<ProjectileSpawnForWeapon>();
    }

    protected void LoadCharge()
    {
        if (charge != null) return;
        charge = GetComponent<Charge>();
    }

    #endregion
}