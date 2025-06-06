using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;

    protected PlayerStateManager playerStateManager;
    protected PlayerStateMachine stateMachine;
    protected PlayerDataSO playerDataSO;
    protected PlayerAudioDataSO playerAudioDataSO;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;
    protected string animBoolName;

    public PlayerState(PlayerStateManager playerStateManager, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName)
    {
        this.playerStateManager = playerStateManager;
        this.stateMachine = stateMachine;
        this.playerDataSO = playerDataSO;
        this.playerAudioDataSO = playerAudioDataSO;
        this.animBoolName = animBoolName;
        core = playerStateManager.Core;
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
