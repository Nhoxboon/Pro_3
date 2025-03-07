using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(stateMachine, playerDataSO, animBoolName)
    {

    }
}
