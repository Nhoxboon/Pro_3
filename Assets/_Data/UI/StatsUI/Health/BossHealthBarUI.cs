using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarUI : HealthBarUI
{
    protected override void LoadCore()
    {
        
    }

    public void SetCore(Core core)
    {
        this.core = core;
    }
    
}
