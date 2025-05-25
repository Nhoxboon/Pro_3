
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : NhoxBehaviour
{
    [Header("Dependencies")] [SerializeField]
    private Image weaponIcon;

    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponDescription;

    private WeaponDataSO weaponData;

    public void PopulateUI(WeaponDataSO data)
    {
        if(data is null) return;

        weaponData = data;

        weaponIcon.sprite = weaponData.icon;
        weaponName.SetText(weaponData.nameWeapon);
        weaponDescription.SetText(weaponData.description);
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponIcon();
        LoadWeaponName();
        LoadWeaponDescription();
    }
    
    protected void LoadWeaponIcon()
    {
        if (weaponIcon != null) return;
        weaponIcon = transform.Find("VerticalLayout/Row1/WeaponIcon/Icon").GetComponent<Image>();
        Debug.Log(transform.name + " :LoadWeaponIcon", gameObject);
    }
    
    protected void LoadWeaponName()
    {
        if (weaponName != null) return;
        weaponName = transform.Find("VerticalLayout/Row1").GetComponentInChildren<TMP_Text>();
        Debug.Log(transform.name + " :LoadWeaponName", gameObject);
    }
    
    protected void LoadWeaponDescription()
    {
        if (weaponDescription != null) return;
        weaponDescription = transform.Find("VerticalLayout/Row2").GetComponentInChildren<TMP_Text>();
        Debug.Log(transform.name + " :LoadWeaponDescription", gameObject);
    }
}
