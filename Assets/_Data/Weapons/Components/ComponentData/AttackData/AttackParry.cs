
using System;
using UnityEngine;

[Serializable]
public class AttackParry : AttackData
{
    public DirectionalInformation[] parryDirectionalInformation;
    
    public PhaseTime parryWindowStart;
    public PhaseTime parryWindowEnd;
    
    public string particles;

    public Vector2 particlesOffset;
    
    public bool IsBlocked(float angle, out DirectionalInformation directionalInformation)
    {
        directionalInformation = null;

        foreach (var directionInformation in parryDirectionalInformation)
        {
            var blocked = directionInformation.IsAngleBetween(angle);
                
            if (!blocked) continue;
                
            directionalInformation = directionInformation;
            return true;
        }

        return false;
    }
}
