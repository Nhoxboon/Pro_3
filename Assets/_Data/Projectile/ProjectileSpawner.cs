using System;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    protected Projectile currentProjectile;
    protected Vector2 spawnDir;
    protected Vector2 spawnPos;

    [SerializeField] protected int chargeAmount;
    [SerializeField] protected float angleVariation;
    protected Vector2 currentDirection;
    
    public static ProjectileSpawner Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.LogError("Only 1 ProjectileSpawner allowed to exist");
            return;
        }

        Instance = this;
    }

    /// <summary>
    ///     Spawn projectile and handle all logic
    /// </summary>
    public void SpawnProjectile(ProjectileSpawnInfo spawnInfo, Vector2 spawnDirection,
        Vector3 spawnerPos,
        int facingDirection,
        Action<Projectile> OnSpawnProjectile)
    {
        SetSpawnPosition(spawnerPos, spawnInfo.Offset, facingDirection);

        SetSpawnDirection(spawnDirection, facingDirection);

        GetProjectileAndSetPositionAndRotation(spawnInfo.ProjectilePrefabName);

        InitializeProjectile(spawnInfo, OnSpawnProjectile);
    }

    protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
    {
        // Add offset to player position, accounting for FacingDirection
        spawnPos = referencePosition;
        spawnPos.Set(spawnPos.x + offset.x * facingDirection, spawnPos.y + offset.y);
    }

    protected virtual void SetSpawnDirection(Vector2 direction, int facingDirection)
    {
        spawnDir.Set(direction.x * facingDirection, direction.y);
    }

    protected virtual void GetProjectileAndSetPositionAndRotation(string prefabName)
    {
        // GET projectile from pool
        var projectileTransform = Spawn(prefabName, spawnPos, Quaternion.identity);
        projectileTransform.gameObject.SetActive(true);
        currentProjectile = projectileTransform.GetComponent<Projectile>();

        if (projectileTransform == null)
        {
            Debug.LogError("Failed to spawn projectile: " + prefabName);
            return;
        }

        // Calculate angle and set rotation
        var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
        projectileTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected virtual void InitializeProjectile(ProjectileSpawnInfo spawnInfo, Action<Projectile> OnSpawnProjectile)
    {
        currentProjectile.Reset();
        currentProjectile.SendDataPackage(spawnInfo.DamageData);
        currentProjectile.SendDataPackage(spawnInfo.KnockBackData);
        currentProjectile.SendDataPackage(spawnInfo.PoiseDamageData);

        // Broadcast new projectile has been spawned so other components can  pass through data packages
        OnSpawnProjectile?.Invoke(currentProjectile);

        currentProjectile.Init();
    }

    public void SpawnProjectileStrategy(
        ProjectileSpawnInfo spawnInfo,
        Vector3 spawnerPos,
        int facingDirection,
        Action<Projectile> OnSpawnProjectile
    )
    {
        SpawnProjectile(spawnInfo, spawnInfo.Direction, spawnerPos, facingDirection, OnSpawnProjectile);
    }
    
    //NOTE: Spawn multiple projectiles at an offset angle (Charge strategy)
    //TODO: Need to processing more...
    public void SpawnWithChargeStrategy(
        ProjectileSpawnInfo spawnInfo,
        Vector3 spawnerPos,
        int facingDirection,
        Action<Projectile> OnSpawnProjectile
    )
    {
        // If there are no charges, we don't spawn any projectiles.
        if (chargeAmount <= 0)
            return;

        // If there is only one charge, the direction to spawn the projectile in is the same as the direction that has been passed in.
        if (chargeAmount == 1)
        {
            currentDirection = spawnInfo.Direction;
        }
        else
        {
            /*
             * If there are more than one charge, we need to rotate the current direction by half of the total angle variation.
             * Total angle variation = (ChargeAmount - 1) * AngleVariation
             * This creates the initialRotationQuaternion. By multiplying this by the passed in spawn direction, we get a new direction that
             * has been rotated anti-clockwise by that amount.
             */
            var initialRotationQuaternion = Quaternion.Euler(0f, 0f, -((chargeAmount - 1f) * angleVariation / 2f));

            // Rotate the vector to set our first spawn direction
            currentDirection = initialRotationQuaternion * spawnInfo.Direction;
        }

        // The quaternion that we will use to rotate the spawn direction by our angle variation for every projectile we spawn
        var rotationQuaternion = Quaternion.Euler(0f, 0f, angleVariation);

        for (var i = 0; i < chargeAmount; i++)
        {
            // Projectile spawn methods. See ProjectileSpawnerStrategy class for more details
            SpawnProjectile(spawnInfo, currentDirection, spawnerPos, facingDirection,
                OnSpawnProjectile);

            // Rotate the spawn direction for next projectile.
            currentDirection = rotationQuaternion * currentDirection;
        }
    }
}