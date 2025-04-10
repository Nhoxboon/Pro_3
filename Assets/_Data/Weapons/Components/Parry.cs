
using System;
using UnityEngine;

public class Parry : WeaponComponent<ParryData, AttackParry>
{
    public event Action<GameObject> OnParry;

    protected DamageModifier damageModifier;
    protected BlockKnockbackModifier knockbackModifier;
    protected BlockPoiseModifier poiseModifier;
    
    protected bool isBlockWindowActive;
    protected bool shouldUpdate;

    protected float nextWindowTriggerTime;

    protected void StartParryWindow()
    {
        isBlockWindowActive = true;
        shouldUpdate = false;

        damageModifier.OnModified += HandleParry;
        
        Core.DamageReceiver.Modifiers.AddModifier(damageModifier);
        Core.Knockbackable.Modifiers.AddModifier(knockbackModifier);
        Core.PoiseReceiver.Modifiers.AddModifier(poiseModifier);
    }

    protected void StopParryWindow()
    {
        isBlockWindowActive = false;
        shouldUpdate = false;

        damageModifier.OnModified -= HandleParry;
        
        Core.DamageReceiver.Modifiers.RemoveModifier(damageModifier);
        Core.Knockbackable.Modifiers.RemoveModifier(knockbackModifier);
        Core.PoiseReceiver.Modifiers.RemoveModifier(poiseModifier);
    }

    protected override void HandleExit()
    {
        base.HandleExit();
        
        Core.DamageReceiver.Modifiers.RemoveModifier(damageModifier);
        Core.Knockbackable.Modifiers.RemoveModifier(knockbackModifier);
        Core.PoiseReceiver.Modifiers.RemoveModifier(poiseModifier);
    }

    protected bool IsAttackParried(Transform source, out DirectionalInformation directionalInformation)
    {
        float angleOfAttacker =
            AngleUtilities.AngleFromFacingDirection(Core.Root.transform, source, Core.Movement.FacingDirection);

        return currentAttackData.IsBlocked(angleOfAttacker, out directionalInformation);
    }

    protected void HandleParry(GameObject parriedGameObject)
    {
        //Note: The modifier is only used to detect an enemy making contact with the player from allowed directions.
        //If that happens we still need to inform the entity that it has been parried.
        if (!CombatParryUtilities.TryParry(parriedGameObject, new CombatParryData(Core.Root), out _, out _)) return;
        
        weapon.Anim.SetTrigger("parry");
        
        OnParry?.Invoke(parriedGameObject);
        Core.ParticleManager.StartWithRandomRotation(currentAttackData.particles, currentAttackData.particlesOffset);
    }

    protected void HandleEnterAttackPhase(AttackPhases phase)
    {
        shouldUpdate = isBlockWindowActive
            ? currentAttackData.parryWindowEnd.TryGetTriggerTime(phase, out nextWindowTriggerTime)
            : currentAttackData.parryWindowStart.TryGetTriggerTime(phase, out nextWindowTriggerTime);
    }

    #region Plumbing

    protected override void Start()
    {
        base.Start();

        damageModifier = new DamageModifier(IsAttackParried);
        knockbackModifier = new BlockKnockbackModifier(IsAttackParried);
        poiseModifier = new BlockPoiseModifier(IsAttackParried);

        EventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
    }

    protected void Update()
    {
        if (!shouldUpdate || !IsPastTriggerTime()) return;

        if (isBlockWindowActive) StopParryWindow();
        else StartParryWindow();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
    }

    protected bool IsPastTriggerTime()
    {
        return Time.time >= nextWindowTriggerTime;
    }

    #endregion
}
