using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] protected bool isKnockbackActive;
    [SerializeField] protected float knockbackStartTime;
    [SerializeField] protected float maxKnockbackTime = 0.2f;

    public void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged");
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.canSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    protected void CheckKnockback()
    {
        if(isKnockbackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.TouchingDirection.IsGrounded) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            core.Movement.canSetVelocity = true;
        }
    }
}
