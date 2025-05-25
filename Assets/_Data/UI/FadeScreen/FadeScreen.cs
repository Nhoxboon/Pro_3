
using UnityEngine;

public class FadeScreen : NhoxBehaviour
{
    [SerializeField] protected Animator anim;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
    }

    protected void LoadAnimator()
    {
        if(anim != null) return;
        anim = GetComponent<Animator>();
        Debug.Log(transform.name + " :LoadAnimator", gameObject);
    }
    
    public void FadeIn() => anim.SetTrigger("fadeIn");
    
    public void FadeOut() => anim.SetTrigger("fadeOut");
}
