public class DamageData : ComponentDataAbstract<AttackDamage>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Damage);
    }
}