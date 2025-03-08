using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetAnimationEvent : NhoxBehaviour
{
    protected void AnimationTrigger()
    {
        PlayerCtrl.Instance.PlayerMovement.AnimationTrigger();
    }

    protected void AnimationFinishTrigger()
    {
        PlayerCtrl.Instance.PlayerMovement.AnimationFinishTrigger();
    }
}
