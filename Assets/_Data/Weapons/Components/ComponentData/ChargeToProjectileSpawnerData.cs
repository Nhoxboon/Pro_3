public class ChargeToProjectileSpawnerData : ComponentDataAbstract<AttackChargeToProjectileSpawner>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(ChargeToProjectileSpawner);
    }
}