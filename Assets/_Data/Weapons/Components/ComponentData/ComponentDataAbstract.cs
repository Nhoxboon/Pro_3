using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ComponentDataAbstract
{
    [SerializeField, HideInInspector] protected string name;

    public Type componentDependency;

    public ComponentDataAbstract()
    {
        SetComponentName();
        SetComponentDependency();
    }

    public void SetComponentName() => name = GetType().Name;

    protected abstract void SetComponentDependency();

    public virtual void SetAttackDataName()
    {
        //For override
    }

    public virtual void InitializeAttackData(int numberOfAttacks)
    {
        //For override
    }
}


[Serializable]
public abstract class ComponentDataAbstract<T> : ComponentDataAbstract where T : AttackData
{
    [SerializeField] protected bool repeatData;

    [SerializeField] protected T[] attackData;
    public T[] AttackData => attackData;

    public T GetAttackData(int index) => attackData[repeatData ? 0 : index];

    public T[] GetAllAttackData() => attackData;

    public override void SetAttackDataName()
    {
        base.SetAttackDataName();
        for (int i = 0; i < attackData.Length; i++)
        {
            attackData[i].SetAttackDataName(i + 1);
        }
    }

    public override void InitializeAttackData(int numberOfAttacks)
    {
        base.InitializeAttackData(numberOfAttacks);

        int oldLength = attackData != null ? attackData.Length : 0;

        if (oldLength == numberOfAttacks) return;

        Array.Resize(ref attackData, numberOfAttacks);

        if(oldLength < numberOfAttacks)
        {
            for (int i = oldLength; i < numberOfAttacks; i++)
            {
                var newObj = Activator.CreateInstance(typeof(T)) as T;
                attackData[i] = newObj;
            }
        }

        SetAttackDataName();
    }
}
