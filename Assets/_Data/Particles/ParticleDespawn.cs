using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDespawn : DespawnByFinishAnimation
{
    public override void DespawnObject()
    {
        ParticleSpawner.Instance.ReturnToPool(transform.parent.gameObject);
        animationFinished = false;
    }
}
