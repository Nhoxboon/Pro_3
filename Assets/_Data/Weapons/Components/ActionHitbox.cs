using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitbox : WeaponComponent<ActionHitboxData, AttackActionHitbox>
{
    public event Action<Collider2D[]> OnDetectedCol2D;

    protected Vector2 offset;

    protected Collider2D[] detectedObjects;

    protected override void Start()
    {
        base.Start();

        EventHandler.OnAttackAction += HandleAttackAction;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventHandler.OnAttackAction -= HandleAttackAction;
    }

    protected void HandleAttackAction()
    {
        offset.Set(transform.position.x + (currentAttackData.Hitbox.center.x * Core.Movement.FacingDirection), transform.position.y + currentAttackData.Hitbox.center.y);

        detectedObjects =  Physics2D.OverlapBoxAll(offset, currentAttackData.Hitbox.size, 0f, data.detectedLayers);

        if (detectedObjects.Length == 0) return;

        OnDetectedCol2D?.Invoke(detectedObjects);
    }

    protected void OnDrawGizmosSelected()
    {
        if (data == null) return;

        foreach (var item in data.GetAllAttackData())
        {
            if (!item.Debug) continue;
            Gizmos.DrawWireCube(transform.position + (Vector3) item.Hitbox.center, item.Hitbox.size);
        }
    }
}
