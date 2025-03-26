using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    [SerializeField] protected SpriteRenderer baseSR;
    [SerializeField] protected SpriteRenderer weaponSR;

    [SerializeField] protected int currentWeaponSpriteIndex;

    protected override void Start()
    {
        base.Start();
        baseSR.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSR.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
    }

    protected void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttacking)
        {
            weaponSR.sprite = null;
            return;
        }

        var currentAttackSprite = currentAttackData.Sprites;
        if (currentWeaponSpriteIndex >= currentAttackSprite.Length)
        {
            Debug.LogWarning(weapon.name + " weapon sprite length mismatch");
            return;
        }

        weaponSR.sprite = currentAttackSprite[currentWeaponSpriteIndex];

        currentWeaponSpriteIndex++;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBaseSR();
        LoadWeaponSR();
    }

    protected void LoadBaseSR()
    {
        if (this.baseSR != null) return;
        this.baseSR = weapon.BaseGameObj.GetComponent<SpriteRenderer>();
        //Debug.Log(transform.name + " LoadBaseSR", gameObject);
    }

    protected void LoadWeaponSR()
    {
        if (this.weaponSR != null) return;
        this.weaponSR = weapon.WeaponSpriteGameObj.GetComponent<SpriteRenderer>();
        //Debug.Log(transform.name + " LoadWeaponSR", gameObject);
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }
}