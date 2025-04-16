
using UnityEngine;
using UnityEngine.UI;

public class EquippedWeaponUI : NhoxBehaviour
{
    [SerializeField] protected Image weaponIcon;
    
    [SerializeField] protected CombatInputs input;
    
    [SerializeField] protected WeaponInventory weaponInventory;

    protected WeaponDataSO weaponData;
    
    protected void SetWeaponIcon()
    {
        weaponIcon.sprite = weaponData ? weaponData.icon : null;
        weaponIcon.color = weaponData ? Color.white : Color.clear;
    }
    
    protected void HandleWeaponDataChanged(int inputIndex, WeaponDataSO data)
    {
        if (inputIndex != (int)input) return;

        weaponData = data;
        SetWeaponIcon();
    }
    
    protected override void Start()
    {
        base.Start();
        weaponInventory.TryGetWeapon((int)input, out weaponData);
        SetWeaponIcon();
    }
    
    protected void OnEnable()
    {
        weaponInventory.OnWeaponDataChanged += HandleWeaponDataChanged;
    }
    
    protected void OnDisable()
    {
        weaponInventory.OnWeaponDataChanged -= HandleWeaponDataChanged;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponInventory();
        LoadWeaponIcon();
    }
    
    protected void LoadWeaponInventory()
    {
        if (weaponInventory != null) return;
        weaponInventory = FindFirstObjectByType<WeaponInventory>();
        Debug.Log(transform.name + " :LoadWeaponInventory", gameObject);
    }
    
    protected void LoadWeaponIcon()
    {
        if (weaponIcon != null) return;
        weaponIcon = transform.Find("WeaponIcon").GetComponent<Image>();
        Debug.Log(transform.name + " :LoadWeaponIcon", gameObject);
    }
}
