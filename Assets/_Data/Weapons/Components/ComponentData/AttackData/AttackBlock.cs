
using System;
using UnityEngine;

[Serializable]
public class AttackBlock : AttackData
{
    public AudioClip blockSound;
    [SerializeField] public DirectionalInformation[] blockDirectionInformation;
    
    [SerializeField] public PhaseTime blockWindowStart;
    [SerializeField] public PhaseTime blockWindowEnd;
    
    [SerializeField] public string particles;
    
    [SerializeField] public Vector2 particlesOffset;
    
    public bool IsBlocked(float angle, out DirectionalInformation directionalInformation)
    {
        directionalInformation = null;

        foreach (var directionInformation in blockDirectionInformation)
        {
            bool blocked = directionInformation.IsAngleBetween(angle);
            
            if (!blocked) continue;
            
            directionalInformation = directionInformation;
            return true;
        }

        return false;
    }
}
