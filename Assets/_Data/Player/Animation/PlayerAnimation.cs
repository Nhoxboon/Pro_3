using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : NhoxBehaviour
{
    [SerializeField] protected Animator anim;

    void Update()
    {
        UpdateAnimations();
    }

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

    protected void UpdateAnimations()
    {
        anim.SetBool("isMoving", PlayerCtrl.Instance.PlayerMovement.IsMoving);
        anim.SetBool("isGrounded", PlayerCtrl.Instance.TouchingDirection.IsGrounded);
        anim.SetBool("isWallSliding", PlayerCtrl.Instance.PlayerMovement.IsWallSliding);
        anim.SetFloat("yVelocity", PlayerCtrl.Instance.PlayerMovement.Rb.velocity.y);
    }
}
