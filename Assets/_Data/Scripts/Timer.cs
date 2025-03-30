using System;
using UnityEngine;

/*
 * TimeNotifier fires off an event after some duration once the timer has started. The timer can also be configured
 * to automatically restart the timer once the duration has passed or to only trigger once.
 */
public class Timer
{
    private float duration;

    private bool enabled;

    private float targetTime;

    public event Action OnNotify;

    public void Init(float dur, bool reset = false)
    {
        enabled = true;

        duration = dur;
        SetTargetTime();

        if (reset)
            OnNotify += SetTargetTime;
        else
            OnNotify += Disable;
    }

    private void SetTargetTime()
    {
        targetTime = Time.time + duration;
    }

    public void Disable()
    {
        enabled = false;

        OnNotify -= Disable;
    }

    public void Tick()
    {
        if (!enabled)
            return;

        if (Time.time >= targetTime) OnNotify?.Invoke();
    }
}