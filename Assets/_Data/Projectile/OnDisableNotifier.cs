using System;
using UnityEngine;

public class OnDisableNotifier : NhoxBehaviour
{
    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
    }

    public event Action OnDisableEvent;

    [ContextMenu("Test")]
    private void Test()
    {
        transform.parent.gameObject.SetActive(false);
    }
}