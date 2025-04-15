using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "ScriptableObject/Weapon/Basic Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public Sprite icon;
    public string name;
    public string description;

    public RuntimeAnimatorController animatorController;
    public int numberOfAttacks;

    [field: SerializeReference] public List<ComponentDataAbstract> componentData;

    public T GetData<T>()
    {
        return componentData.OfType<T>().FirstOrDefault();
    }

    public List<Type> GetAllDependencies()
    {
        return componentData.Select(component => component.componentDependency).ToList();
    }

    public void AddData(ComponentDataAbstract data)
    {
        if (componentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) return;

        componentData.Add(data);
    }
}