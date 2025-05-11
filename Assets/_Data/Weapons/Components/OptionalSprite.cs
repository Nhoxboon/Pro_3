
using UnityEngine;

public class OptionalSprite : WeaponComponent<OptionalSpriteData, AttackOptionalSprite>
{
    protected SpriteRenderer spriteRenderer;
    
    private void HandleSetOptionalSpriteActive(bool value)
    {
        spriteRenderer.enabled = value;
    }
    
    protected override void HandleEnter()
    {
        base.HandleEnter();

        if (!currentAttackData.UseOptionalSprite)
            return;

        spriteRenderer.sprite = currentAttackData.sprite;
    }
    
    #region Plumbing
    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponentInChildren<OptionalSpriteMarker>().SpriteRenderer;
        spriteRenderer.enabled = false;
    }

    protected override void Start()
    {
        base.Start();

        EventHandler.OnSetOptionalSpriteActive += HandleSetOptionalSpriteActive;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventHandler.OnSetOptionalSpriteActive -= HandleSetOptionalSpriteActive;
    }

    #endregion
}
