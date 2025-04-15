
using System;
using UnityEngine;

public class WeaponSwapChoiceRequest
{
    public WeaponSwapChoice[] Choices { get; }
    public WeaponDataSO NewWeaponData { get; }
    
    public Action<WeaponSwapChoice> Callback;

    public WeaponSwapChoiceRequest(
        Action<WeaponSwapChoice> callback,
        WeaponSwapChoice[] choices,
        WeaponDataSO newWeaponData
    )
    {
        Callback = callback;
        Choices = choices;
        NewWeaponData = newWeaponData;
    }
}
