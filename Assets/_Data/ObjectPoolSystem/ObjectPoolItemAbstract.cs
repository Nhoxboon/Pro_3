using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolItemAbstract : NhoxBehaviour
{
    protected ObjectPool objectPool;
    protected Component component;

    public abstract void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;
    public abstract void Release();
}
