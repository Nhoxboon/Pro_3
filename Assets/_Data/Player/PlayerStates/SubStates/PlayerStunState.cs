using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(PlayerStateManager playerStateManager, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(playerStateManager,
        stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);

        if (Time.time >= startTime + playerDataSO.stunTime)
        {
            stateMachine.ChangeState(playerStateManager.PlayerIdleState);
        }
    }
}
