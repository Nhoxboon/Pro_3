using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbackable : CoreComponent
{
    [SerializeField] protected bool isKnockbackActive;
    [SerializeField] protected float knockbackStartTime;
    [SerializeField] protected float maxKnockbackTime = 0.2f;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckKnockback();
    }

    public virtual void Knockback(Vector2 angle, float strength, int direction)
    {
        if (core != null && core.Movement != null)
        {
            core.Movement.SetVelocity(strength, angle, direction);
            core.Movement.canSetVelocity = false;
            isKnockbackActive = true;
            knockbackStartTime = Time.time;
        }
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
