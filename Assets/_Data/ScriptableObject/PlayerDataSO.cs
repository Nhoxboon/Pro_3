using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "ScriptableObject/Player Data/Base Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;
}
