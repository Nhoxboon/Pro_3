using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : NhoxBehaviour
{
    [SerializeField] protected List<Transform> prefabs = new List<Transform>();

    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> poolObjs = new List<Transform>(); 

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
        LoadPrefabs();
    }

    protected virtual void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find("Holder");
        Debug.Log(transform.name + " :LoadHolder", gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            prefabs.Add(prefab);
        }
        HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    protected Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }

    protected Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in poolObjs)
        {
            if (poolObj.name == prefab.name && !poolObj.gameObject.activeSelf)
            {
                poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newObj = Instantiate(prefab);
        newObj.name = prefab.name;
        return newObj;
    }

    public virtual void ReturnToPool(GameObject obj)
    {
        if (poolObjs.Contains(obj.transform)) return;

        obj.SetActive(false);
        obj.transform.SetParent(holder);
        poolObjs.Add(obj.transform);
    }
}
