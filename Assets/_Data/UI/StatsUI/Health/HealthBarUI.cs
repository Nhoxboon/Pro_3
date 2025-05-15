using System;
using UnityEngine;

public abstract class HealthBarUI : StatsUI
{
    protected virtual void OnEnable()
    {
        core.Stats.Health.OnValueDecreased += UpdateBarUI;
    }
    
    protected virtual void OnDisable()
    {
        core.Stats.Health.OnValueDecreased -= UpdateBarUI;
    }
    
    protected void FlipUI() => rectTransform.Rotate(0, 180, 0);

    protected override void UpdateBarUI()
    {
        slider.maxValue = core.Stats.Health.MaxValue;
        slider.value = core.Stats.Health.CurrentValue;
    }
}
