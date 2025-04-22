
using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwapChoiceUI : NhoxBehaviour
{
    public event Action<WeaponSwapChoice> OnChoiceSelected;
    
    [SerializeField] protected WeaponInfoUI weaponInfoUI;
    [SerializeField] protected CombatInputs input;
    [SerializeField] protected Button button;
    
    protected WeaponSwapChoice weaponSwapChoice;

    protected void OnEnable()
    {
        button.onClick.AddListener(HandleClick);
    }
    
    protected void OnDisable()
    {
        button.onClick.RemoveListener(HandleClick);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponInfoUI();
        LoadButton();
    }
    
    protected void LoadWeaponInfoUI()
    {
        if (weaponInfoUI != null) return;
        weaponInfoUI = GetComponent<WeaponInfoUI>();
        Debug.Log(transform.name + " :LoadWeaponInfoUI", gameObject);
    }
    
    protected void LoadButton()
    {
        if (button != null) return;
        button = GetComponentInChildren<Button>();
        Debug.Log(transform.name + " :LoadButton", gameObject);
    }
    
    public void TakeRelevantChoice(WeaponSwapChoice[] choices)
    {
        var inputIndex = (int)input;

        if (choices.Length <= inputIndex)
        {
            return;
        }

        SetChoice(choices[inputIndex]);
    }
    
    protected void SetChoice(WeaponSwapChoice choice)
    {
        weaponSwapChoice = choice;

        weaponInfoUI.PopulateUI(choice.WeaponData);
    }

    protected void HandleClick()
    {
        OnChoiceSelected?.Invoke(weaponSwapChoice);
    }
}
