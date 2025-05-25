
using UnityEngine;

public class PlayerHealthBarUI : HealthBarUI
{
    protected override void LoadCore()
    {
        if (core != null) return;
        core = PlayerCtrl.Instance.PlayerStateManager.Core;
        Debug.Log(transform.name + " LoadCore", gameObject);
    }
}
