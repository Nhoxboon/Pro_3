using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    protected int amountOfJumpsLeft;

    public PlayerJumpState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
        amountOfJumpsLeft = playerDataSO.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        AudioManager.Instance.PlaySFX(playerAudioDataSO.jumpAudio);
        
        core.Movement.SetVelocityY(playerDataSO.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        playerStateManager.PlayerInAirState.SetJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerDataSO.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
