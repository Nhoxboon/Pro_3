using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Weapon weapon;
    protected WeaponGenerator weaponGenerator;
    protected int inputIndex;

    private bool canInterrupt;

    private bool checkFlip;

    public PlayerAttackState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName, Weapon weapon, CombatInputs input) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
    {
        this.weapon = weapon;
        weaponGenerator = weapon.GetComponent<WeaponGenerator>();

        inputIndex = (int)input;
        weapon.OnUseInput += HandleUseInput;

        weapon.GetAnimationEvent.OnEnableInterrupt += HandleEnableInterrupt;
        weapon.GetAnimationEvent.OnFinish += HandleFinish;

        weapon.GetAnimationEvent.OnFlipSetActive += HandleFlipSetActive;
    }

    public override void Enter()
    {
        base.Enter();

        weaponGenerator.OnWeaponGenerating += HandleWeaponGenerating;

        checkFlip = true;
        canInterrupt = false;

        weapon.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        weaponGenerator.OnWeaponGenerating -= HandleWeaponGenerating;

        weapon.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        int xInput = InputManager.Instance.NormInputX;

        weapon.CurrentInput = InputManager.Instance.AttackInputs[inputIndex];

        if (checkFlip)
        {
            core.Movement.CheckIfShouldFlip(xInput);
        }

        if (!canInterrupt) return;

        if (xInput != 0 || InputManager.Instance.AttackInputs[0] || InputManager.Instance.AttackInputs[1])
        {
            isAbilityDone = true;
        }
    }

    protected void HandleFinish()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }

    protected void HandleUseInput() => InputManager.Instance.UseAttackInput(inputIndex);

    protected void HandleEnableInterrupt() => canInterrupt = true;

    protected void HandleFlipSetActive(bool value)
    {
        checkFlip = value;
    }
    
    public bool CanTransitionToAttackState() => weapon.CanEnterAttack;
    
    protected void HandleWeaponGenerating()
    {
        stateMachine.ChangeState(player.PlayerIdleState);
    }
}
