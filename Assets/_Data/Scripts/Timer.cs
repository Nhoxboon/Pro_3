using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public event Action OnTimerEnd;

    protected float startTime;
    protected float targetTime;
    protected float duration;

    protected bool isActive;

    public Timer(float duration)
    {
        this.duration = duration;
    }

    public void StartTimer()
    {
        startTime = Time.time;
        targetTime = startTime + duration;
        isActive = true;
    }

    public void StopTimer()
    {
        isActive = false;
    }

    public void Tick()
    {
        if(!isActive) return;

        if (Time.time >= targetTime)
        {
            OnTimerEnd?.Invoke();
            StopTimer();
        }
    }

}
