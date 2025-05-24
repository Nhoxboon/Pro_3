
using System;
using UnityEngine;

public class WeaponSwapUI : NhoxBehaviour
{
    [SerializeField] protected WeaponSwap weaponSwap;
    [SerializeField] protected WeaponInfoUI newWeaponInfo;
    [SerializeField] protected WeaponSwapChoiceUI[] weaponSwapChoiceUIs;
            
    [SerializeField] protected CanvasGroup canvasGroup;
        
    protected Action<WeaponSwapChoice> choiceSelectedCallback;
    
    protected void OnEnable()
    {
        weaponSwap.OnChoiceRequested += HandleChoiceRequested;

        foreach (var weaponSwapChoiceUI in weaponSwapChoiceUIs)
        {
            weaponSwapChoiceUI.OnChoiceSelected += HandleChoiceSelected;
        }
    }

    protected void OnDisable()
    {
        weaponSwap.OnChoiceRequested -= HandleChoiceRequested;
            
        foreach (var weaponSwapChoiceUI in weaponSwapChoiceUIs)
        {
            weaponSwapChoiceUI.OnChoiceSelected -= HandleChoiceSelected;
        }
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCanvasGroup();
        LoadWeaponSwap();
        LoadNewWeaponInfo();
        LoadWeaponSwapChoiceUIs();
    }
    
    protected void LoadWeaponSwap()
    {
        if (weaponSwap != null) return;
        weaponSwap = FindFirstObjectByType<WeaponSwap>();
        Debug.Log(transform.name + " :LoadWeaponSwap", gameObject);
    }
    
    protected void LoadNewWeaponInfo()
    {
        if (newWeaponInfo != null) return;
        newWeaponInfo = transform.Find("NewWeaponInfoUI").GetComponent<WeaponInfoUI>();
        Debug.Log(transform.name + " :LoadNewWeaponInfo", gameObject);
    }
    
    protected void LoadWeaponSwapChoiceUIs()
    {
        if (weaponSwapChoiceUIs != null && weaponSwapChoiceUIs.Length > 0) return;
        weaponSwapChoiceUIs = GetComponentsInChildren<WeaponSwapChoiceUI>();
        Debug.Log(transform.name + " :LoadWeaponSwapChoiceUIs", gameObject);
    }
    
    protected void LoadCanvasGroup()
    {
        if (canvasGroup != null) return;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        Debug.Log(transform.name + " :LoadCanvasGroup", gameObject);
    }
    #endregion

    protected void HandleChoiceRequested(WeaponSwapChoiceRequest choiceRequest)
    {
        GameManager.Instance.ChangeState(GameManager.GameState.UI);
            
        choiceSelectedCallback = choiceRequest.Callback;
            
        newWeaponInfo.PopulateUI(choiceRequest.NewWeaponData);
            
        foreach (var weaponSwapChoiceUi in weaponSwapChoiceUIs)
        {
            weaponSwapChoiceUi.TakeRelevantChoice(choiceRequest.Choices);
        }
            
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
    }

    protected void HandleChoiceSelected(WeaponSwapChoice choice)
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Gameplay);
            
        choiceSelectedCallback?.Invoke(choice);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }
}
