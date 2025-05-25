using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    protected bool canDash;
    protected bool isHolding;
    protected bool dashInputStop;
    protected float lastDashTime;

    protected Vector2 dashDirection;
    protected Vector2 dashDirectionInput;
    protected Vector2 lastAIPos;

    public PlayerDashState(PlayerStateManager playerStateManagerMovement, PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO, PlayerAudioDataSO playerAudioDataSO, string animBoolName) : base(
        playerStateManagerMovement, stateMachine, playerDataSO, playerAudioDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        canDash = false;
        InputManager.Instance.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * core.Movement.FacingDirection;

        Time.timeScale = playerDataSO.holdTimeScale;
        startTime = Time.unscaledTime;

        PlayerCtrl.Instance.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if(core.Movement.CurrentVelocity.y > 0)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerDataSO.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            PlayerCtrl.Instance.PlayerAnimation.YVelocityAnimation(core.Movement.CurrentVelocity.y);
            PlayerCtrl.Instance.PlayerAnimation.XVelocityAnimation(Mathf.Abs(core.Movement.CurrentVelocity.x));

            if (isHolding)
            {
                dashDirectionInput = InputManager.Instance.DashDirectionInput;
                dashInputStop = InputManager.Instance.DashInputStop;

                if (dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                PlayerCtrl.Instance.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (dashInputStop || Time.unscaledTime >= startTime + playerDataSO.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    playerStateManager.Rb.drag = playerDataSO.drag;
                    core.Movement.SetVelocity(playerDataSO.dashVelocity, dashDirection);
                    PlayerCtrl.Instance.DashDirectionIndicator.gameObject.SetActive(false);
                    PlayAfterImage();
                    AudioManager.Instance.PlaySFX(playerAudioDataSO.dashAudio);
                }
            }
            else
            {
                core.Movement.SetVelocity(playerDataSO.dashVelocity, dashDirection);
                CheckIfShouldAfterImage();

                if (Time.time >= startTime + playerDataSO.dashTime)
                {
                    playerStateManager.Rb.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    protected void CheckIfShouldAfterImage()
    {
        if (Vector2.Distance(playerStateManager.transform.position, lastAIPos) >= playerDataSO.distanceBetweenImages)
        {
            PlayAfterImage();
        }
    }

    protected void PlayAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = playerStateManager.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return canDash && Time.time >= lastDashTime + playerDataSO.dashCooldown;
    }

    public void ResetCanDash() => canDash = true;
}
