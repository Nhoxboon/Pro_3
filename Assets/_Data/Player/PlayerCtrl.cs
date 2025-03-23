using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : NhoxBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected Player player;
    public Player Player => player;

    [SerializeField] protected PlayAnimation playerAnimation;
    public PlayAnimation PlayerAnimation => playerAnimation;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null)
        {
            Debug.LogError("Only 1 PlayerCtrl allow to exist");
            return;
        }
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerMovement();
        this.LoadPlayAnimation();
    }

    protected void LoadPlayerMovement()
    {
        if (this.player != null) return;
        this.player = this.GetComponentInChildren<Player>();
        Debug.Log(transform.name + " LoadPlayerMovement", gameObject);
    }


    protected void LoadPlayAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = this.GetComponentInChildren<PlayAnimation>();
        Debug.Log(transform.name + " LoadPlayAnimation", gameObject);
    }
}
