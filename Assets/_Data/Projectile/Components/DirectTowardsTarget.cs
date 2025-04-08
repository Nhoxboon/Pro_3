
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectTowardsTarget : ProjectileComponent
{
    [SerializeField] protected float minStep;
    [SerializeField] protected float maxStep = 180f;
    [SerializeField] protected float timeToMaxStep = 0.4f;
    
    [SerializeField] protected List<Transform> targets;
    protected Transform currentTarget;

    [SerializeField] protected float step;
    protected float startTime;
    
    [SerializeField] protected Vector2 direction;
    
    protected override void Init()
    {
        base.Init();

        currentTarget = null;

        startTime = Time.time;

        step = minStep;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!HasTarget()) return;

        step = Mathf.Lerp(minStep, maxStep, (Time.time - startTime) / timeToMaxStep);
        direction = (currentTarget.position - transform.parent.position).normalized;
        
        Rotate(direction);
    }

    protected bool HasTarget()
    {
        if(currentTarget) return true;
        
        targets.RemoveAll(item => item == null || !item.gameObject.activeInHierarchy);
        
        if (targets.Count <= 0) return false;
        
        targets = targets.OrderBy(target => (target.position - transform.parent.position).sqrMagnitude).ToList();
        currentTarget = targets[0];
        return true;
    }

    protected void Rotate(Vector2 dir)
    {
        if(dir.Equals(Vector2.zero)) return;
        
        var toRotation = QuaternionExtensions.Vector2ToRotation(dir);

        transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, toRotation, step * Time.deltaTime);
    }

    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);
        
        if(dataPackage is not TargetsDataPackage targetsDataPackage) return;
        
        targets = targetsDataPackage.targets;
    }
    
    protected void OnDrawGizmos()
    {
        if (!currentTarget) return;

        Gizmos.DrawLine(transform.position, currentTarget.position);
    }
}
