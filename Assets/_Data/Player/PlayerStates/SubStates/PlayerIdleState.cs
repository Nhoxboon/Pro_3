using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(stateMachine, playerDataSO, animBoolName)
    {
    }
}
