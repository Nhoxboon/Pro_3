using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;

    public PlayerAbilityState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = PlayerCtrl.Instance.TouchingDirection.IsGrounded;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && playerMovement.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(playerMovement.PlayerIdleState);
            }
            else
            {
                stateMachine.ChangeState(playerMovement.PlayerInAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
