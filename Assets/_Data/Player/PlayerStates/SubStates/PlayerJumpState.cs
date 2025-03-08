using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    protected int amountOfJumpsLeft;

    public PlayerJumpState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
        amountOfJumpsLeft = playerDataSO.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        playerMovement.SetVelocityY(playerDataSO.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumpsLeft();
        playerMovement.PlayerInAirState.SetJumping();
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
