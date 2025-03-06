using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : NhoxBehaviour
{
    [SerializeField] protected bool combatEnabled;
    [SerializeField] protected bool gotInput;

    [SerializeField] protected float lastInputTime;
    [SerializeField] protected float inputTimer;

    protected void CheckAttackInput()
    {
        if(InputManager.Instance.AttackPressed)
        {
            if(combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    protected void CheckAttacks()
    {
        if(gotInput)
        {
            //Attack 1
        }

        if(Time.time >= lastInputTime + inputTimer)
        {
            //Wait for new input
        }
    }
}
