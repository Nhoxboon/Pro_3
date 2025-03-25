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

    protected void AnimationFinishTrigger() => OnFinish?.Invoke();

    protected void StartMovementTrigger() => OnStartMovement?.Invoke();

    protected void StopMovementTrigger() => OnStopMovement?.Invoke();

    protected void AttackActionTrigger() => OnAttackAction?.Invoke();

}
