
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeter : WeaponComponent<TargeterData, AttackTargeter>
{
    protected List<Transform> targets = new List<Transform>();
    
    [SerializeField] protected bool isActive;

    protected override void HandleEnter()
    {
        base.HandleEnter();
        
        isActive = true;
    }

    protected override void HandleExit()
    {
        base.HandleExit();
        
        isActive = false;
    }

    public List<Transform> GetTarget()
    {
        return targets;
    }

    protected void CheckForTargets()
    {
        Vector3 pos = transform.parent.position +
                      new Vector3(currentAttackData.area.center.x * Core.Movement.FacingDirection,
                          currentAttackData.area.center.y);

        Collider2D[] targetColliders =
            Physics2D.OverlapBoxAll(pos, currentAttackData.area.size, 0, currentAttackData.damageableLayer);
        
        targets = targetColliders.Select(item => item.transform).ToList();
    }
    
    #region Plumbing

    protected void FixedUpdate()
    {
        if(!isActive) return;
        
        CheckForTargets();
    }
    
    protected void OnDrawGizmosSelected()
    {
        if (data == null) return;

        foreach (var attackTargeter in data.GetAllAttackData())
        {
            Gizmos.DrawWireCube(transform.parent.position + (Vector3)attackTargeter.area.center, attackTargeter.area.size);
        }

        Gizmos.color = Color.red;
        foreach (var target in targets)
        {
            Gizmos.DrawWireSphere(target.position, 0.25f);
        }
    }
    
    #endregion 
}
