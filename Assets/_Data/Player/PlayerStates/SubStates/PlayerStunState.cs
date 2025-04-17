using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);

        if (Time.time >= startTime + playerDataSO.stunTime)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }
    }
}
