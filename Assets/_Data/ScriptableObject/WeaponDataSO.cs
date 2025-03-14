using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "ScriptableObject/Weapon Data/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    public float[] movementSpeed;
}
