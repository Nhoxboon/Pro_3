using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHold : WeaponComponent
{
    protected bool input;
    protected bool minHoldPassed;

    protected override void Awake()
    {
        base.Awake();
        weapon.OnCurrentInputChange += HandleCurrentInputChange;
        EventHandler.OnMinHoldPassed += HandleMinHoldPassed;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        EventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();

        minHoldPassed = false;
    }

    protected void HandleCurrentInputChange(bool newInput)
    {
        input = newInput;
        Core.Movement.SetVelocityZero();
        SetAnimatorParameter();
    }

    protected void HandleMinHoldPassed()
    {
        minHoldPassed = true;

        SetAnimatorParameter();
    }

    protected void SetAnimatorParameter()
    {
        if (input)
        {
            weapon.Anim.SetBool("hold", input);
            return;
        }

        if(minHoldPassed)
        {
            weapon.Anim.SetBool("hold", input);
            return;
        }
    }
}
