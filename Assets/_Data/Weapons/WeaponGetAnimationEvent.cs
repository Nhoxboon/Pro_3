using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGetAnimationEvent : NhoxBehaviour
{
    public event Action OnFinish;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnAttackAction;

    public event Action OnMinHoldPassed;
    public event Action<AttackPhases> OnEnterAttackPhase;

    protected void AnimationFinishTrigger() => OnFinish?.Invoke();

    protected void StartMovementTrigger() => OnStartMovement?.Invoke();

    protected void StopMovementTrigger() => OnStopMovement?.Invoke();

    protected void AttackActionTrigger() => OnAttackAction?.Invoke();

    //For weapon have multiple attack phase
    protected void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();
    protected void EnterAttackPhase(AttackPhases attackPhase) => OnEnterAttackPhase?.Invoke(attackPhase);

}
