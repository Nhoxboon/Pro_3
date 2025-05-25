
using UnityEngine;

public class Audio : WeaponComponent<AudioData, AttackAudio>
{
    protected override void HandleEnter()
    {
        base.HandleEnter();
        
        AudioManager.Instance.PlaySFX(currentAttackData?.audioClip);
    }
}
