using UnityEngine;

public class ProjectileSpawner : Spawner
{
    private static ProjectileSpawner instance;
    public static ProjectileSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 ProjectileSpawner allow to exist");
            return;
        }
        instance = this;
    }

    public Transform SpawnProjectile(string projectileName, Vector3 position, Quaternion rotation)
    {
        Transform prefab = GetPrefabByName(projectileName);
        if (prefab == null)
        {
            Debug.LogError($"Prefab {projectileName} not found!");
            return null;
        }

        Transform projectile = GetObjectFromPool(prefab);
        projectile.SetParent(holder);
        projectile.position = position;
        projectile.rotation = rotation;
        projectile.gameObject.SetActive(true);

        return projectile;
    }
}