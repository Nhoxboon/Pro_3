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

    public void AnimationState(string animBoolName, bool value)
    {
        anim.SetBool(animBoolName, value);
    }

    public void YVelocityAnimation(float yVelocity)
    {
        anim.SetFloat("yVelocity", yVelocity);
    }

    public void XVelocityAnimation(float xVelocity)
    {
        anim.SetFloat("xVelocity", xVelocity);
    }

}
