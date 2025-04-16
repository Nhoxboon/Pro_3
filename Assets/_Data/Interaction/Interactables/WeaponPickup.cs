using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponPickup : NhoxBehaviour, IInteractable<WeaponDataSO>
{
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected SpriteRenderer weaponIcon;
    [SerializeField] protected Bobber bobber;

    [SerializeField] protected WeaponDataSO weaponData;
    public Rigidbody2D Rb => rb;

    public WeaponDataSO GetContext()
    {
        return weaponData;
    }

    public void SetContext(WeaponDataSO context)
    {
        weaponData = context;
        weaponIcon.sprite = weaponData.icon;
    }

    public void Interact()
    {
        Destroy(gameObject);
    }

    public void EnableInteraction()
    {
        bobber.StartBobbing();
    }

    public void DisableInteraction()
    {
        bobber.StopBobbing();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody();
        LoadWeaponData();
        LoadWeaponIcon();
        LoadBobber();
    }

    protected void LoadRigidbody()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " :LoadRigidbody", gameObject);
    }

    protected void LoadWeaponIcon()
    {
        if (weaponIcon != null) return;
        weaponIcon = GetComponentInChildren<SpriteRenderer>();
        weaponIcon.sprite = weaponData.icon;
        Debug.Log(transform.name + " :LoadWeaponIcon", gameObject);
    }

    protected void LoadBobber()
    {
        if (bobber != null) return;
        bobber = GetComponentInChildren<Bobber>();
        Debug.Log(transform.name + " :LoadBobber", gameObject);
    }

    protected void LoadWeaponData()
    {
        if (weaponData != null) return;
        weaponData = Resources.Load<WeaponDataSO>("Weapons/Sword_1");
        Debug.Log(transform.name + " :LoadWeaponData", gameObject);
    }
}