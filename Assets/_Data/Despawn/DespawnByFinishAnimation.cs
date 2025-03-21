using UnityEngine;

public abstract class DespawnByFinishAnimation : Despawn
{
    [SerializeField] protected bool animationFinished = false;

    public void OnAnimationFinished()
    {
        animationFinished = true;
    }

    protected override bool CanDespawn()
    {
        return animationFinished;
    }
}