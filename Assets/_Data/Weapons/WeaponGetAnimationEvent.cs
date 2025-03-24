using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGetAnimationEvent : NhoxBehaviour
{
    public event Action OnFinish;
    public event Action OnStartMovement;
    public event Action OnStopMovement;

    protected void AnimationFinishTrigger() => OnFinish?.Invoke();

    protected void StartMovementTrigger() => OnStartMovement?.Invoke();

    protected void StopMovementTrigger() => OnStopMovement?.Invoke();

}
