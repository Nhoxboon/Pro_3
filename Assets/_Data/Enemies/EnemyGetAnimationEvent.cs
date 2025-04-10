using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetAnimationEvent : NhoxBehaviour
{
    public AttackState attackState;

    protected void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    protected void FinishAttack()
    {
        attackState.FinishAttack();
    }
    
    private void SetParryWindowActive(int value)
    {
        attackState.SetParryWindowActive(Convert.ToBoolean(value));
    }   
}
