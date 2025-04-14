
using UnityEngine;

public class KnockbackOnParry : WeaponComponent<KnockbackOnParryData, AttackKnockback>
{
    protected Parry parry;

    protected override void Start()
    {
        base.Start();
        
        parry.OnParry += HandleParry;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        parry.OnParry -= HandleParry;
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParry();
    }
    
    protected void LoadParry()
    {    
        if (parry != null) return;
        parry = GetComponent<Parry>();
    }
    
    protected void HandleParry(GameObject parriedGameObject)
    {
        CombatKnockBackUtilities.TryKnockBack(parriedGameObject,
            new CombatKnockbackData(currentAttackData.angle, currentAttackData.strength, Core.Movement.FacingDirection,
                Core.Root), out _);
    }
}
