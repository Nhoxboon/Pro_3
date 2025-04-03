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

    public event Action OnUseInput;
    public event Action OnEnableInterrupt;
    public event Action<bool> OnSetOptionalSpriteActive;
    public event Action<bool> OnFlipSetActive;

    public event Action OnMinHoldPassed;
    public event Action<AttackPhases> OnEnterAttackPhase;

    protected void AnimationFinishTrigger() => OnFinish?.Invoke();

    protected void StartMovementTrigger() => OnStartMovement?.Invoke();

    protected void StopMovementTrigger() => OnStopMovement?.Invoke();

    protected void AttackActionTrigger() => OnAttackAction?.Invoke();
    protected void EnableInterrupt() => OnEnableInterrupt?.Invoke();
    protected void SetFlipActive() => OnFlipSetActive?.Invoke(true);
    protected void SetFlipInactive() => OnFlipSetActive?.Invoke(false);

    //For weapon have multiple attack phase
    protected void UseInputTrigger() => OnUseInput?.Invoke();
    protected void SetOptionalSpriteEnabled() => OnSetOptionalSpriteActive?.Invoke(true);
    protected void SetOptionalSpriteDisabled() => OnSetOptionalSpriteActive?.Invoke(false);
    protected void MinHoldPassedTrigger() => OnMinHoldPassed?.Invoke();
    protected void EnterAttackPhase(AttackPhases attackPhase) => OnEnterAttackPhase?.Invoke(attackPhase);

}
