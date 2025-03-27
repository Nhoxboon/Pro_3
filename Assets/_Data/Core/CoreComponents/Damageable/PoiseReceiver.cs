using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseReceiver : CoreComponent
{
    public virtual void Poise(float amount)
    {
        core.Stats.Poise.Decrease(amount);
    }
}
