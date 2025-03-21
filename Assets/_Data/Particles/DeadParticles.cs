using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadParticles : NhoxBehaviour
{
    [SerializeField] protected new ParticleSystem particleSystem;


    private void OnEnable()
    {
        ResetParticle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParticleSystem();
    }

    protected void LoadParticleSystem()
    {
        if (particleSystem != null) return;
        particleSystem = GetComponent<ParticleSystem>();
        Debug.Log(transform.name + " :LoadParticleSystem", gameObject);
    }

    public void ResetParticle()
    {
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        particleSystem.Play();
    }
}
