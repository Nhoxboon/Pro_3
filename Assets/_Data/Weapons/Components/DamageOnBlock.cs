
using UnityEngine;

public class DamageOnBlock : WeaponComponent<DamageOnBlockData, AttackDamage>
{
    protected Block block;
    
    protected void HandleBlock(GameObject blockedGameObject)
    {
        CombatDamageUtilities.TryDamage(blockedGameObject, new CombatDamageData(currentAttackData.Amount, Core.Root), out _);
    }

    protected override void Start()
    {
        base.Start();
        block.OnBlock += HandleBlock;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        block.OnBlock -= HandleBlock;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBlock();
    }
    
    protected void LoadBlock()
    {    
        if (block != null) return;
        block = GetComponent<Block>();
    }
}
