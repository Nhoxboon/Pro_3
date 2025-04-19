using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetAnimationEvent : NhoxBehaviour
{
    protected void AnimationTrigger()
    {
        PlayerCtrl.Instance.PlayerStateManager.AnimationTrigger();
    }

    protected void AnimationFinishTrigger()
    {
        PlayerCtrl.Instance.PlayerStateManager.AnimationFinishTrigger();
    }
}
