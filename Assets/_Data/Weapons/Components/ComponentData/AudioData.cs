
using UnityEngine;

public class AudioData : ComponentDataAbstract<AttackAudio>
{
    protected override void SetComponentDependency()
    {
        componentDependency = typeof(Audio);
    }
}
