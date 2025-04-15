
using UnityEngine;

public class WeaponSwapChoice
{
    public WeaponDataSO WeaponData { get; }
    public int Index { get; }

    public WeaponSwapChoice(WeaponDataSO weaponData, int index)
    {
        WeaponData = weaponData;
        Index = index;
    }
}
