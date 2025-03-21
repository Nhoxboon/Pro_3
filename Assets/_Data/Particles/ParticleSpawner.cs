using UnityEngine;

public class ParticleSpawner : Spawner
{
    private static ParticleSpawner instance;
    public static ParticleSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 ParticleSpawner allow to exist");
            return;
        }
        instance = this;
    }

    public Transform SpawnParticle(string particleName, Vector3 position, Quaternion rotation)
    {
        Transform prefab = GetPrefabByName(particleName);
        if (prefab == null)
        {
            Debug.LogError($"Prefab {particleName} not found!");
            return null;
        }

        Transform particle = GetObjectFromPool(prefab);
        particle.SetPositionAndRotation(position, rotation);
        particle.gameObject.SetActive(true);
        return particle;
    }
}