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

    public PlayerDashState(Player playerMovement, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(playerMovement, stateMachine, playerDataSO, animBoolName)
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

        player.DashDirectionIndicator.gameObject.SetActive(true);
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
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (dashInputStop || Time.unscaledTime >= startTime + playerDataSO.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.Rb.drag = playerDataSO.drag;
                    core.Movement.SetVelocity(playerDataSO.dashVelocity, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlayAfterImage();
                }
            }
            else
            {
                core.Movement.SetVelocity(playerDataSO.dashVelocity, dashDirection);
                CheckIfShouldAfterImage();

                if (Time.time >= startTime + playerDataSO.dashTime)
                {
                    player.Rb.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    protected void CheckIfShouldAfterImage()
    {
        if (Vector2.Distance(player.transform.parent.position, lastAIPos) >= playerDataSO.distanceBetweenImages)
        {
            PlayAfterImage();
        }
    }

    protected void PlayAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.parent.position;
    }

    public bool CheckIfCanDash()
    {
        return canDash && Time.time >= lastDashTime + playerDataSO.dashCooldown;
    }

    public void ResetCanDash() => canDash = true;
}
