using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : NhoxBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected ParticleDespawn despawn;

    private void OnEnable()
    {
        ResetParticle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadDespawn();
    }

    protected void LoadAnimator()
    {
        if (anim != null) return;
        anim = GetComponent<Animator>();
        Debug.Log(transform.name + " :LoadAnimator", gameObject);
    }

    protected void LoadDespawn()
    {
        if (despawn != null) return;
        despawn = GetComponentInChildren<ParticleDespawn>();
        Debug.Log(transform.name + " :LoadDespawn", gameObject);
    }

    protected void OnAnimationFinished()
    {
        despawn.OnAnimationFinished();
    }

    public void ResetParticle()
    {
            anim.Play("HitParticles", -1, 0f);
    }
}
