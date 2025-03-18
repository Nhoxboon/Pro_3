using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DamageReceiver : CoreComponent
{
    public virtual void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged");
        core.Stats.DecreaseHealth(amount);
    }
}
