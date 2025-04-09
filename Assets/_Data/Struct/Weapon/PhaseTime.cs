using System;
using UnityEngine;

[Serializable]
public struct PhaseTime
{
    [SerializeField] public float duration;
    
    [SerializeField] public AttackPhases phase;
    
    public bool TryGetTriggerTime(AttackPhases phase, out float triggerTime)
    {
        triggerTime = Time.time + duration;
        return phase == this.phase;
    }
}