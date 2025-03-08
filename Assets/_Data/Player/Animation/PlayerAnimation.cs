using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : NhoxBehaviour
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
        anim = transform.parent.GetComponentInChildren<Animator>();
        Debug.Log(transform.name + " LoadAnimator", gameObject);
    }

    public void TriggerAttack()
    {
        anim.SetTrigger("attack");
    }

    public void AnimationState(string animBoolName, bool value)
    {
        anim.SetBool(animBoolName, value);
    }

}
