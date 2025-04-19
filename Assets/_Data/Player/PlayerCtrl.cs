using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCtrl : NhoxBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected PlayerStateManager playerStateManager;
    public PlayerStateManager PlayerStateManager => playerStateManager;

    [SerializeField] protected PlayAnimation playerAnimation;
    public PlayAnimation PlayerAnimation => playerAnimation;
    
    [SerializeField] protected Transform dashDirectionIndicator;
    public Transform DashDirectionIndicator => dashDirectionIndicator;

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
        LoadPlayerStateMachine();
        LoadPlayAnimation();
        LoadDashDirectionIndicator();
    }

    protected void LoadPlayerStateMachine()
    {
        if (playerStateManager != null) return;
        playerStateManager = this.GetComponent<PlayerStateManager>();
        Debug.Log(transform.name + " LoadPlayerMovement", gameObject);
    }


    protected void LoadPlayAnimation()
    {
        if (playerAnimation != null) return;
        playerAnimation = GetComponentInChildren<PlayAnimation>();
        Debug.Log(transform.name + " LoadPlayAnimation", gameObject);
    }
    
    protected void LoadDashDirectionIndicator()
    {
        if(dashDirectionIndicator != null) return;
        dashDirectionIndicator = transform.Find("DashDirectionIndicator");
        Debug.Log(transform.name + " LoadDashDirectionIndicator", gameObject);
    }
}
