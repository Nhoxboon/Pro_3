﻿
using System;
using UnityEngine;

public class Block : WeaponComponent<BlockData, AttackBlock>
{
    // Event fired off when an attack is blocked. The parameter is the GameObject of the entity that was blocked
    public event Action<GameObject> OnBlock;

    // The modifier that we give the DamageReceiver when the block window is active.
    protected DamageModifier blockDamageModifier;
    protected BlockKnockbackModifier knockbackModifier;
    protected BlockPoiseModifier poiseModifier;

    protected bool isBlockWindowActive;
    protected bool shouldUpdate;

    protected float nextWindowTriggerTime;

    protected void StartBlockWindow()
    {
        isBlockWindowActive = true;
        shouldUpdate = false;
        
        blockDamageModifier.OnModified += HandleBlock;
        
        Core.DamageReceiver.Modifiers.AddModifier(blockDamageModifier);
        Core.Knockbackable.Modifiers.AddModifier(knockbackModifier);
        Core.PoiseReceiver.Modifiers.AddModifier(poiseModifier);
    }

    protected void StopBlockWindow()
    {
        isBlockWindowActive = false;
        shouldUpdate = false;
        
        blockDamageModifier.OnModified -= HandleBlock;
        
        Core.DamageReceiver.Modifiers.RemoveModifier(blockDamageModifier);
        Core.Knockbackable.Modifiers.RemoveModifier(knockbackModifier);
        Core.PoiseReceiver.Modifiers.RemoveModifier(poiseModifier);
    }

    protected bool IsAttackBlocked(Transform source, out DirectionalInformation directionalInformation)
    {
        float angleOfAttacker =
            AngleUtilities.AngleFromFacingDirection(Core.Root.transform, source, Core.Movement.FacingDirection);
        
        return currentAttackData.IsBlocked(angleOfAttacker, out directionalInformation);
    }

    protected void HandleBlock(GameObject source)
    {
        AudioManager.Instance.PlaySFX(currentAttackData.blockSound);
        Core.ParticleManager.StartWithRandomRotation(currentAttackData.particles, currentAttackData.particlesOffset);
        
        OnBlock?.Invoke(source);
    }

    protected void HandleEnterAttackPhase(AttackPhases phases)
    {
        shouldUpdate = isBlockWindowActive
            ? currentAttackData.blockWindowEnd.TryGetTriggerTime(phases, out nextWindowTriggerTime)
            : currentAttackData.blockWindowStart.TryGetTriggerTime(phases, out nextWindowTriggerTime);
    }

    #region Plumbing

    protected override void Start()
    {
        base.Start();
        
        blockDamageModifier = new DamageModifier(IsAttackBlocked);
        knockbackModifier = new BlockKnockbackModifier(IsAttackBlocked);
        poiseModifier = new BlockPoiseModifier(IsAttackBlocked);
        
        EventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
    }

    protected void Update()
    {
        if(!shouldUpdate || !IsPastTriggerTime()) return;

        if (isBlockWindowActive) StopBlockWindow();
        else StartBlockWindow();
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
    }
    
    private bool IsPastTriggerTime()
    {
        return Time.time >= nextWindowTriggerTime;
    }

    #endregion
    
    #region Gizmos
    [SerializeField] private float gizmoRadius = 2f;
    
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying || currentAttackData == null) 
            return;

        var position = Core.Root.transform.position;
        var facingDirection = Core.Movement.FacingDirection;

        foreach (var directionInfo in currentAttackData.blockDirectionInformation)
        {
            float startAngle = directionInfo.minAngle + (facingDirection == 1 ? 0f : 180f);
            float endAngle = directionInfo.maxAngle + (facingDirection == 1 ? 0f : 180f);

            Gizmos.color = isBlockWindowActive ? Color.green : Color.red;
            
            DrawDirectionalGizmo(
                position, 
                startAngle, 
                endAngle, 
                gizmoRadius
            );
        }
    }

    private void DrawDirectionalGizmo(Vector3 center, float startAngle, float endAngle, float radius)
    {
        const int segments = 30;
        Vector3 prevPoint = center + GetDirection(startAngle) * radius;

        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            Vector3 nextPoint = center + GetDirection(angle) * radius;
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }

        Gizmos.DrawLine(center, center + GetDirection(startAngle) * radius);
        Gizmos.DrawLine(center, center + GetDirection(endAngle) * radius);
    }

    private Vector3 GetDirection(float angle)
    {
        return Quaternion.Euler(0, 0, angle) * Vector3.right;
    }
    #endregion
}
