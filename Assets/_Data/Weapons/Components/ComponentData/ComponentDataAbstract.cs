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

    public virtual void SetAttackDataNames()
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

    public override void SetAttackDataNames()
    {
        base.SetAttackDataNames();
        for (int i = 0; i < attackData.Length; i++)
        {
            attackData[i].SetAttackDataName(i + 1);
        }
    }

    public override void InitializeAttackData(int numberOfAttacks)
    {
        base.InitializeAttackData(numberOfAttacks);
        
        int newLen = repeatData ? 1 : numberOfAttacks;

        int oldLength = attackData != null ? attackData.Length : 0;

        if (oldLength == newLen) return;

        Array.Resize(ref attackData, newLen);

        if(oldLength < newLen)
        {
            for (int i = oldLength; i < attackData.Length; i++)
            {
                var newObj = Activator.CreateInstance(typeof(T)) as T; 
                attackData[i] = newObj;
            }
        }

        SetAttackDataNames();
    }
}
