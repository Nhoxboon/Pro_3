using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerMovement playerMovement;
    protected PlayerStateMachine stateMachine;
    protected PlayerDataSO playerDataSO;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;
    protected string animBoolName;

    public PlayerState(PlayerMovement playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName)
    {
        this.playerMovement = playerMovement;
        this.stateMachine = stateMachine;
        this.playerDataSO = playerDataSO;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        PlayerCtrl.Instance.PlayerAnimation.AnimationState(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        PlayerCtrl.Instance.PlayerAnimation.AnimationState(animBoolName, false);
        isExitingState = true;
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

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
