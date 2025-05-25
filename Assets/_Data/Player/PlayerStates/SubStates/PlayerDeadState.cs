using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerStateManager playerStateManager, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(playerStateManager, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(playerAudioDataSO.deathClip);
        core.Death.Die();
        playerStateManager.gameObject.SetActive(false);
        GameObject.Find("UI").GetComponent<UI>().SwitchToEndScreen();
    }
}
