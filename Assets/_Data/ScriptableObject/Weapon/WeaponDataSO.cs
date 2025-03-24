using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "ScriptableObject/Weapon/Basic Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public int numberOfAttacks;

    [field: SerializeReference] public List<ComponentData> componentData;

    [ContextMenu("Add Sprite Data")]
    protected void AddSpriteData() => componentData.Add(new WeaponSpriteData());

    public T GetData<T>()
    {
        return componentData.OfType<T>().FirstOrDefault();
    }
}
