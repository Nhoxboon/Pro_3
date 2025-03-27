using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    [SerializeField] protected SpriteRenderer baseSR;
    [SerializeField] protected SpriteRenderer weaponSR;

    [SerializeField] protected int currentWeaponSpriteIndex;
    [SerializeField] protected Sprite[] currentPhaseSprites;

    protected override void Start()
    {
        base.Start();
        baseSR.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

        EventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSR.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

        EventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
    }

    protected void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;

        currentPhaseSprites = currentAttackData.PhaseSprites.FirstOrDefault(data => data.Phase == phase).Sprites;
    }

    protected void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttacking)
        {
            weaponSR.sprite = null;
            return;
        }

        if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
            Debug.LogWarning(weapon.name + " weapon sprite length mismatch");
            return;
        }

        weaponSR.sprite = currentPhaseSprites[currentWeaponSpriteIndex];

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