
using UnityEngine;

public interface IInteractableItem : IInteractable
{
    void EnableInteraction();

    void DisableInteraction();

    Vector3 GetPosition();
}

public interface IInteractableItem<T> : IInteractableItem
{
    T GetContext();

    void SetContext(T context);
}