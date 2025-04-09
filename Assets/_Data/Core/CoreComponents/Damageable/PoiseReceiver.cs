using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseReceiver : CoreComponent
{
    public Modifiers<Modifier<CombatPoiseData>, CombatPoiseData> Modifiers { get; } = new();
    
    public virtual void Poise(CombatPoiseData data)
    {
        data = Modifiers.ApplyAllModifiers(data);
        
        core.Stats.Poise.Decrease(data.Amount);
    }
}
