using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Weapon weapon;

    public PlayerAttackState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName, Weapon weapon) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
        this.weapon = weapon;

        weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();

        weapon.Enter();
    }

    protected void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
