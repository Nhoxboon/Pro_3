public class ChargeData : ComponentDataAbstract<AttackCharge>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Charge);
    }
}