using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseReceiver : CoreComponent
{
    public Modifiers<Modifier<CombatPoiseDamageData>, CombatPoiseDamageData> Modifiers { get; } = new();
    
    public virtual void Poise(CombatPoiseDamageData data)
    {
        data = Modifiers.ApplyAllModifiers(data);
        
        core.Stats.Poise.Decrease(data.Amount);
    }
}
