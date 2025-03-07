using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(stateMachine, playerDataSO, animBoolName)
    {
    }
}
