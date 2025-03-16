using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : NhoxBehaviour
{
    [SerializeField] protected AggressiveWeapon weapon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAggressiveWeapon();
    }

    protected void LoadAggressiveWeapon()
    {
        if (weapon != null) return;
        weapon = GetComponentInParent<AggressiveWeapon>();
        Debug.Log(transform.name + " :LoadAggressiveWeapon", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);

    }
}
