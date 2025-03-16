using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "ScriptableObject/Weapon Data/Aggressive Weapon")]
public class AggressiveWeaponDataSO : WeaponDataSO
{
    [SerializeField] protected WeaponAttackDetails[] attackDetails;
    public WeaponAttackDetails[] AttackDetails => attackDetails;

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;

        movementSpeed = new float[amountOfAttacks];

        for(int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
