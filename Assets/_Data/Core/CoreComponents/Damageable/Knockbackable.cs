using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbackable : CoreComponent
{
    public Modifiers<Modifier<CombatKnockbackData>, CombatKnockbackData> Modifiers { get; } = new();
    
    [SerializeField] protected bool isKnockbackActive;
    [SerializeField] protected float knockbackStartTime;
    [SerializeField] protected float maxKnockbackTime = 0.2f;
    
    [SerializeField] protected bool canKnockback = true;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckKnockback();
    }

    public virtual void Knockback(CombatKnockbackData data)
    {
        if (!canKnockback) return;
        data = Modifiers.ApplyAllModifiers(data);
        
        core.Movement.SetVelocity(data.Strength, data.Angle, data.Direction);
        core.Movement.canSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    public void CheckKnockback()
    {
        if (isKnockbackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.TouchingDirection.IsGrounded) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            core.Movement.canSetVelocity = true;
        }
    }
}
