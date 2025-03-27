using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Weapon weapon;
    protected int inputIndex;

    public PlayerAttackState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName, Weapon weapon, CombatInputs input) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
        this.weapon = weapon;

        inputIndex = (int)input;

        weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();

        weapon.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        weapon.CurrentInput = InputManager.Instance.AttackInputs[inputIndex];
    }

    protected void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
