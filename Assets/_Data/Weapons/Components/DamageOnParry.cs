
using UnityEngine;

public class DamageOnParry : WeaponComponent<DamageOnParryData, AttackDamage>
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
        CombatDamageUtilities.TryDamage(parriedGameObject, new CombatDamageData(currentAttackData.Amount, Core.Root),
            out _);
    }
}
