
using UnityEngine;

public class EnemyHealthBarUI : HealthBarUI
{
    protected override void OnEnable()
    {
        base.OnEnable();
        core.Movement.OnFlip += FlipUI;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        core.Movement.OnFlip -= FlipUI;
    }
    
    protected override void LoadCore()
    {
        if (core != null) return;
        core = transform.parent.parent.GetComponentInChildren<Core>();
        Debug.Log(transform.name + " LoadCore", gameObject);
    }
}
