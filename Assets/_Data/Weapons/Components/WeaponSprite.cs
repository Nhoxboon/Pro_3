using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : WeaponComponent
{
    [SerializeField] protected SpriteRenderer baseSR;
    [SerializeField] protected SpriteRenderer weaponSR;

    [SerializeField] protected int currentWeaponSpriteIndex;

    [SerializeField] protected WeaponSpriteData data;

    protected override void OnEnable()
    {
        base.OnEnable();
        baseSR.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

        weapon.OnEnter += HandleEnter;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        baseSR.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

        weapon.OnEnter -= HandleEnter;
    }

    protected void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttacking)
        {
            weaponSR.sprite = null;
            return;
        }

        if (weapon.CurrentAttack >= data.AttackData.Length)
        {
            Debug.LogError("CurrentAttack index out of range: " + weapon.CurrentAttack);
            return;
        }

        Debug.Log($"weapon.CurrentAttack: {weapon.CurrentAttack}, AttackData Length: {data.AttackData.Length}", gameObject);


        var currentAttackSprite = data.AttackData[weapon.CurrentAttack].Sprites;
        if (currentWeaponSpriteIndex >= currentAttackSprite.Length)
        {
            Debug.LogWarning(weapon.name + " weapon sprite length mismatch");
            return;
        }

        weaponSR.sprite = currentAttackSprite[currentWeaponSpriteIndex];

        currentWeaponSpriteIndex++;
    }

    protected override void Awake()
    {
        base.Awake();
        data = weapon.WeaponDataSO.GetData<WeaponSpriteData>();
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
        Debug.Log(transform.name + " LoadBaseSR", gameObject);
    }

    protected void LoadWeaponSR()
    {
        if (this.weaponSR != null) return;
        this.weaponSR = weapon.WeaponSpriteGameObj.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + " LoadWeaponSR", gameObject);
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }
}