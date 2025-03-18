using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : NhoxBehaviour
{
    [SerializeField] protected GameObject hitParticles;

    [SerializeField] protected Animator anim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadHitParticles();
    }

    protected void LoadAnimator()
    {
        if (anim != null) return;
        anim = GetComponent<Animator>();
        Debug.Log(transform.name + " :LoadAnimator", gameObject);
    }

    protected virtual void LoadHitParticles()
    {
        if (hitParticles != null) return;
        hitParticles = Resources.Load<GameObject>("Particles/HitParticles");
        Debug.Log(transform.name + " :LoadHitParticles", gameObject);
    }

    public void Damage(float amount)
    {
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        anim.SetTrigger("damage");
        Destroy(gameObject);
    }

}
