using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGetAnimationEvent : NhoxBehaviour
{
    public event Action OnFinish;

    protected void AnimationFinishTrigger() => OnFinish?.Invoke();


}
