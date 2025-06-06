using System;
using UnityEngine;

public class ProjectileSpawner : Spawner
{
    protected Vector2 currentDirection;
    protected Projectile currentProjectile;
    protected Vector2 spawnDir;
    protected Vector2 spawnPos;

    protected static ProjectileSpawner instance;
    public static ProjectileSpawner Instance => instance;

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
        spawnPos = referencePosition;
        spawnPos.Set(spawnPos.x + offset.x * facingDirection, spawnPos.y + offset.y);
    }

    protected virtual void SetSpawnDirection(Vector2 direction, int facingDirection)
    {
        spawnDir.Set(direction.x * facingDirection, direction.y);
    }

    protected virtual void GetProjectileAndSetPositionAndRotation(string prefabName)
    {
        var projectileTransform = Spawn(prefabName, spawnPos, Quaternion.identity);
        projectileTransform.gameObject.SetActive(true);
        currentProjectile = projectileTransform.GetComponent<Projectile>();

        if (projectileTransform == null)
        {
            Debug.LogError("Failed to spawn projectile: " + prefabName);
            return;
        }

        var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
        projectileTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected virtual void InitializeProjectile(ProjectileSpawnInfo spawnInfo, Action<Projectile> OnSpawnProjectile)
    {
        currentProjectile.Reset();
        currentProjectile.SendDataPackage(spawnInfo.DamageData);
        currentProjectile.SendDataPackage(spawnInfo.KnockBackData);
        currentProjectile.SendDataPackage(spawnInfo.PoiseDamageData);
        currentProjectile.SendDataPackage(spawnInfo.ProjectileAudioData);

        OnSpawnProjectile?.Invoke(currentProjectile);

        currentProjectile.Init();
    }

    public void SpawnSingleProjectile(
        ProjectileSpawnInfo spawnInfo,
        Vector3 spawnerPos,
        int facingDirection,
        Action<Projectile> OnSpawnProjectile
    )
    {
        SpawnProjectile(spawnInfo, spawnInfo.Direction, spawnerPos, facingDirection, OnSpawnProjectile);
    }

    public void SpawnChargedProjectiles(
        ProjectileSpawnInfo spawnInfo,
        Vector3 spawnerPos,
        int facingDirection,
        int chargeAmount,
        float angleVariation,
        Action<Projectile> OnSpawnProjectile
    )
    {
        if (chargeAmount <= 0) return;

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

            currentDirection = initialRotationQuaternion * spawnInfo.Direction;
        }

        var rotationQuaternion = Quaternion.Euler(0f, 0f, angleVariation);

        for (var i = 0; i < chargeAmount; i++)
        {
            SpawnProjectile(spawnInfo, currentDirection, spawnerPos, facingDirection,
                OnSpawnProjectile);

            currentDirection = rotationQuaternion * currentDirection;
        }
    }
}