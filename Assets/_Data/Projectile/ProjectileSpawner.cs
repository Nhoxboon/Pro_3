using System;
using UnityEngine;
using Nhoxboon.Projectile;

public class ProjectileSpawner : Spawner
{
    private static ProjectileSpawner instance;
    public static ProjectileSpawner Instance => instance;

    // Biến tạm lưu trữ thông tin spawn
    private Vector2 spawnPos;
    private Vector2 spawnDir;
    private Projectile currentProjectile;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 ProjectileSpawner allowed to exist");
            return;
        }
        instance = this;
    }
    
    /// <summary>
    /// Spawn projectile và xử lý toàn bộ logic
    /// </summary>
    public void SpawnProjectile(ProjectileSpawnInfo spawnInfo, Vector2 spawnDirection,
        Vector3 spawnerPos,
        int facingDirection,
        Action<Projectile> OnSpawnProjectile)
    {
        SetSpawnPosition(spawnerPos, spawnInfo.Offset, facingDirection);

        SetSpawnDirection(spawnDirection, facingDirection);

        GetProjectileAndSetPositionAndRotation(spawnInfo.ProjectilePrefab);

        InitializeProjectile(spawnInfo, OnSpawnProjectile);
    }
    
    protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
    {
        spawnPos = referencePosition;
        spawnPos.Set(
            spawnPos.x + offset.x * facingDirection,
            spawnPos.y + offset.y
        );
    }

    protected virtual void SetSpawnDirection(Vector2 direction, int facingDirection)
    {
        spawnDir.Set(
            direction.x * facingDirection,
            direction.y
        );
    }
    
    protected virtual void GetProjectileAndSetPositionAndRotation(Projectile prefab)
    {
        // Lấy projectile từ pool
        Transform projectileTransform = Spawn(prefab.name, spawnPos, Quaternion.identity);
        projectileTransform.gameObject.SetActive(true);
        currentProjectile = projectileTransform.GetComponent<Projectile>();
        
        if (projectileTransform == null)
        {
            Debug.LogError("Failed to spawn projectile: " + prefab.name);
            return;
        }
        // Tính góc quay từ hướng spawn
        float angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
        projectileTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    protected virtual void InitializeProjectile(ProjectileSpawnInfo spawnInfo, Action<Projectile> OnSpawnProjectile)
    {
        // Gửi dữ liệu từ ProjectileSpawnInfo
        currentProjectile.Reset();
        currentProjectile.SendData(spawnInfo.DamageData);
        currentProjectile.SendData(spawnInfo.KnockBackData);
        currentProjectile.SendData(spawnInfo.PoiseDamageData);
        currentProjectile.SendData(spawnInfo.SpriteDataPackage);

        // Kích hoạt sự kiện
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

    /// <summary>
    /// Spawn nhiều projectile theo góc lệch (Charge strategy)
    /// </summary>
    public void SpawnWithChargeStrategy(
        ProjectileSpawnInfo spawnInfo,
        Vector3 spawnerPos,
        int facingDirection,
        int chargeAmount,
        float angleVariation, 
        Vector2 currentDirection,
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