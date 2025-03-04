using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetAnimationEvent : NhoxBehaviour
{
    public void OnFinishLedgeClimb()
    {
        PlayerCtrl.Instance.PlayerMovement.FinishLedgeClimb();
    }
}
