using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : NhoxBehaviour
{
    [SerializeField] protected GameObject afterImagePrefab;
    [SerializeField] protected Queue<GameObject> availableObjects = new Queue<GameObject>();

    private static PlayerAfterImagePool instance;
    public static PlayerAfterImagePool Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("PlayerAfterImagePool already exists in the scene. Deleting duplicate...");
            return;
        }
        instance = this;

        GrowPool();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAfterImagePrefab();
    }

    protected void LoadAfterImagePrefab()
    {
        if (afterImagePrefab != null) return;
        afterImagePrefab = Resources.Load<GameObject>("AfterImage");
        Debug.Log(transform.name + " LoadAfterImagePrefab", gameObject);
    }

    protected void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
