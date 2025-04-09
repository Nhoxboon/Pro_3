using System;

public class WeaponGetAnimationEvent : NhoxBehaviour
{
    public event Action OnFinish;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnAttackAction;

    public event Action OnUseInput;
    public event Action OnEnableInterrupt;
    public event Action<bool> OnSetOptionalSpriteActive;
    public event Action<bool> OnFlipSetActive;

    public event Action OnMinHoldPassed;
    public event Action<AttackPhases> OnEnterAttackPhase;

    /*
     * Animations events used to indicate when a specific time window starts and stops in an animation. These windows are identified using the
     * AnimationWindows enum. These windows include things like when the shield's block is active and when it can parry.
     */
    // For shield weapon
    public event Action<AnimationWindows> OnStartAnimationWindow;
    public event Action<AnimationWindows> OnStopAnimationWindow;

    protected void AnimationFinishTrigger()
    {
        OnFinish?.Invoke();
    }

    protected void StartMovementTrigger()
    {
        OnStartMovement?.Invoke();
    }

    protected void StopMovementTrigger()
    {
        OnStopMovement?.Invoke();
    }

    protected void AttackActionTrigger()
    {
        OnAttackAction?.Invoke();
    }

    protected void SetFlipActive()
    {
        OnFlipSetActive?.Invoke(true);
    }

    protected void SetFlipInactive()
    {
        OnFlipSetActive?.Invoke(false);
    }

    //For weapon have multiple attack phase
    protected void UseInputTrigger()
    {
        OnUseInput?.Invoke();
    }

    protected void SetOptionalSpriteEnabled()
    {
        OnSetOptionalSpriteActive?.Invoke(true);
    }

    protected void SetOptionalSpriteDisabled()
    {
        OnSetOptionalSpriteActive?.Invoke(false);
    }

    protected void MinHoldPassedTrigger()
    {
        OnMinHoldPassed?.Invoke();
    }

    protected void EnterAttackPhase(AttackPhases attackPhase)
    {
        OnEnterAttackPhase?.Invoke(attackPhase);
    }

    // For shield weapon
    protected void StartAnimationWindow(AnimationWindows window)
    {
        OnStartAnimationWindow?.Invoke(window);
    }

    protected void StopAnimationWindow(AnimationWindows window)
    {
        OnStopAnimationWindow?.Invoke(window);
    }

    protected void EnableInterrupt()
    {
        OnEnableInterrupt?.Invoke();
    }
}