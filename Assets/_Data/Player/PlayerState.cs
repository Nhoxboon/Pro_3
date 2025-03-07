using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerDataSO playerDataSO;

    protected float startTime;
    protected string animBoolName;

    public PlayerState(PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.playerDataSO = playerDataSO;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        PlayerCtrl.Instance.PlayerAnimation.AnimationState(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
    }

    public virtual void Exit()
    {
        PlayerCtrl.Instance.PlayerAnimation.AnimationState(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
