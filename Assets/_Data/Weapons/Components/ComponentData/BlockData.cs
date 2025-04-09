
using UnityEngine;

public class BlockData : ComponentDataAbstract<AttackBlock>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Block);
    }
}
