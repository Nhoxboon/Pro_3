using UnityEngine;

public class PlayerGetAnimationEvent : NhoxBehaviour
{
    [SerializeField] protected PlayerStateManager playerStateManager;

    protected void AnimationTrigger()
    {
        PlayerCtrl.Instance.PlayerStateManager.AnimationTrigger();
    }

    protected void AnimationFinishTrigger()
    {
        PlayerCtrl.Instance.PlayerStateManager.AnimationFinishTrigger();
    }

    protected void MoveAnimationAudioEvent()
    {
        AudioManager.Instance.PlaySFX(playerStateManager.PlayerAudioDataSO.moveClip);
    }

    protected void WallClimbAnimationAudioEvent()
    {
        AudioManager.Instance.PlaySFX(playerStateManager.PlayerAudioDataSO.climbAudio);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerStateManager();
    }

    protected void LoadPlayerStateManager()
    {
        if (playerStateManager != null) return;
        playerStateManager = transform.parent.GetComponent<PlayerStateManager>();
        Debug.Log(transform.name + " :LoadPlayerStateManager", gameObject);
    }
}