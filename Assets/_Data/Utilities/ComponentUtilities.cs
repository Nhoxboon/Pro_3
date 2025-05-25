
using UnityEngine;

public static class ComponentUtilities
{
    public static bool IsInteractable(this Component component, out IInteractableItem interactable)
    {
        return component.TryGetComponent(out interactable);
    }
}
