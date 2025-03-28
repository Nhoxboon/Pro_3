using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : NhoxBehaviour
{
    public float damage;

    public void HandleDetectCol2D(Collider2D[] detectedObjects)
    {
        foreach (var item in detectedObjects)
        {
            DamageReceiver damageReceiver = item.GetComponentInChildren<DamageReceiver>();

            if (damageReceiver != null)
            {
                damageReceiver.Damage(this.damage);
                return;
            }
        }
    }
}
