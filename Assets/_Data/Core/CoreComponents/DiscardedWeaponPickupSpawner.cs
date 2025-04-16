using UnityEngine;

public class DiscardedWeaponPickupSpawner : CoreComponent
{
    [SerializeField] protected Vector2 spawnDirection = Vector2.one;
    [SerializeField] protected float spawnVelocity = 6f;
    [SerializeField] protected WeaponPickup weaponPickupPrefab;
    [SerializeField] protected Vector2 spawnOffset;

    [SerializeField] protected Transform weaponPool;

    [SerializeField] protected WeaponSwap weaponSwap;

    protected void OnEnable()
    {
        weaponSwap.OnWeaponDiscarded += HandleWeaponDiscarded;
    }

    protected void OnDisable()
    {
        weaponSwap.OnWeaponDiscarded -= HandleWeaponDiscarded;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponSwap();
        LoadWeaponPickupPrefab();
        LoadWeaponPool();
    }

    protected void LoadWeaponSwap()
    {
        if (weaponSwap != null) return;
        weaponSwap = core.GetComponentInChildren<WeaponSwap>();
        Debug.Log(transform.name + " :LoadWeaponSwap", gameObject);
    }

    protected void LoadWeaponPickupPrefab()
    {
        if (weaponPickupPrefab != null) return;
        weaponPickupPrefab = Resources.Load<WeaponPickup>("Weapons/WeaponPickup");
        Debug.Log(transform.name + " :LoadWeaponPickupPrefab", gameObject);
    }
    
    protected void LoadWeaponPool()
    {
        if (weaponPool != null) return;
        weaponPool = GameObject.FindGameObjectWithTag("WeaponHolder").transform;
        Debug.Log(transform.name + " :LoadWeaponPool", gameObject);
    }

    //Note: Consider changing this to a obj pooling system
    protected void HandleWeaponDiscarded(WeaponDataSO discardedWeaponData)
    {
        var spawnPoint = core.Movement.FindRelativePoint(spawnOffset);

        var weaponPickup = Instantiate(weaponPickupPrefab, spawnPoint, Quaternion.identity);
        
        weaponPickup.transform.SetParent(weaponPool);
        weaponPickup.transform.name = discardedWeaponData.name;

        weaponPickup.SetContext(discardedWeaponData);

        var adjustedSpawnDirection = new Vector2(spawnDirection.x * core.Movement.FacingDirection, spawnDirection.y);

        weaponPickup.Rb.velocity = adjustedSpawnDirection.normalized * spawnVelocity;
    }
}