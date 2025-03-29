using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : NhoxBehaviour
{
    [SerializeField] protected Animator anim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
    }

    protected void LoadAnimator()
    {
        if (anim != null) return;
        anim = GetComponent<Animator>();
        Debug.Log(transform.name + " :LoadAnimator", gameObject);
    }

    public void Damage(float amount)
    {
        Transform obj =  ParticleSpawner.Instance.Spawn("HitParticles", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        obj.gameObject.SetActive(true);
        anim.SetTrigger("damage");
    }

}
