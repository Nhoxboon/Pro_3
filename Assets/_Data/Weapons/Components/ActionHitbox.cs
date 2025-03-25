using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitbox : WeaponComponent<ActionHitboxData, AttackActionHitbox>
{
    protected override void OnEnable()
    {
        base.OnEnable();

        EventHandler.OnAttackAction += HandleAttackAction;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        EventHandler.OnAttackAction -= HandleAttackAction;
    }

    protected void HandleAttackAction()
    {
        Debug.Log("Handle attack");
    }
}
