using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : NhoxBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    [SerializeField] protected PlayAnimation playerAnimation;
    public PlayAnimation PlayerAnimation => playerAnimation;

    [SerializeField] protected TouchingDirection touchingDirection;
    public TouchingDirection TouchingDirection => touchingDirection;

    [SerializeField] protected PlayerInventory playerInventory;
    public PlayerInventory PlayerInventory => playerInventory;

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
        this.LoadTouchingDirection();
        this.LoadPlayerInventory();
    }

    protected void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = this.GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + " LoadPlayerMovement", gameObject);
    }


    protected void LoadPlayAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = this.GetComponentInChildren<PlayAnimation>();
        Debug.Log(transform.name + " LoadPlayAnimation", gameObject);
    }

    protected void LoadTouchingDirection()
    {
        if (this.touchingDirection != null) return;
        this.touchingDirection = this.GetComponentInChildren<TouchingDirection>();
        Debug.Log(transform.name + " LoadTouchingDirection", gameObject);
    }

    protected void LoadPlayerInventory()
    {
        if (this.playerInventory != null) return;
        this.playerInventory = this.GetComponentInChildren<PlayerInventory>();
        Debug.Log(transform.name + " LoadPlayerInventory", gameObject);
    }
}
