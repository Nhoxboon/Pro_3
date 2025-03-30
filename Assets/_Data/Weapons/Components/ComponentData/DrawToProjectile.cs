/*
 * This component works with both the Draw and ProjectileSpawner components. It listens for the evaluation event from Draw and the projectile spawned event from the projectile spawner.
 * When draw is evaluated that value is stored, when a projectile is spawned, the drawPercentage is packaged up and sent through so any component there can use it.
 */

public class DrawToProjectile : WeaponComponent
{
    private readonly DrawModifierDataPackage drawModifierDataPackage = new();
    protected Draw draw;
    protected ProjectileSpawnForWeapon projectileSpawner;

    private void HandleEvaluateCurve(float value)
    {
        drawModifierDataPackage.DrawPercentage = value;
    }

    private void HandleSpawnProjectile(Projectile projectile)
    {
        projectile.SendDataPackage(drawModifierDataPackage);
    }

    protected override void HandleEnter()
    {
        drawModifierDataPackage.DrawPercentage = 0f;
    }

    #region Plumbing

    protected override void Start()
    {
        base.Start();

        draw = GetComponent<Draw>();
        projectileSpawner = GetComponent<ProjectileSpawnForWeapon>();

        draw.OnEvaluateCurve += HandleEvaluateCurve;
        projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        draw.OnEvaluateCurve -= HandleEvaluateCurve;
        projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;
    }

    #endregion
}