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
}