
using UnityEngine;

public class OptionalSpriteData : ComponentDataAbstract<AttackOptionalSprite>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(OptionalSprite);
    }
}
