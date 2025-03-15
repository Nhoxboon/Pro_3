using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetAnimationEvent : NhoxBehaviour
{
    protected void AnimationTrigger()
    {
        PlayerCtrl.Instance.Player.AnimationTrigger();
    }

    protected void AnimationFinishTrigger()
    {
        PlayerCtrl.Instance.Player.AnimationFinishTrigger();
    }
}
