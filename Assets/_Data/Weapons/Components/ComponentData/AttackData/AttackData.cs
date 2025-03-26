using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    [SerializeField, HideInInspector] protected string name;

    public void SetAttackDataName(int i) => name = $"Attack {i}";
}
