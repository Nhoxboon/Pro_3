using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitbox : WeaponComponent<ActionHitboxData, AttackActionHitbox>
{
    protected event Action<Collider2D[]> OnDetectedCol2D;

    protected Vector2 offset;

    protected Collider2D[] detectedObjects;

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
        offset.Set(transform.position.x + (currentAttackData.Hitbox.center.x * coreMovement.FacingDirection), transform.position.y + currentAttackData.Hitbox.center.y);

        detectedObjects =  Physics2D.OverlapBoxAll(offset, currentAttackData.Hitbox.size, 0f, data.detectedLayers);

        if (detectedObjects.Length == 0) return;

        OnDetectedCol2D?.Invoke(detectedObjects);

        foreach (var item in detectedObjects)
        {
            Debug.Log(item.name);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        if (data == null) return;

        foreach (var item in data.AttackData)
        {
            if (!item.Debug) continue;
            Gizmos.DrawWireCube(transform.position + (Vector3) item.Hitbox.center, item.Hitbox.size);
        }
    }
}
