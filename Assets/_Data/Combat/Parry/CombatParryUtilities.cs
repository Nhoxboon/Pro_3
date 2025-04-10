using System.Collections.Generic;
using UnityEngine;

public static class CombatParryUtilities
{
    public static bool TryParry(GameObject gameObject, CombatParryData data, out ParryReceiver parryReceiver,
        out GameObject parriedGameObject)
    {
        parriedGameObject = null;

        if (gameObject.TryGetComponentInChildren(out parryReceiver))
        {
            parriedGameObject = gameObject;
            parryReceiver.Parry(data);
            return true;
        }

        return false;
    }

    public static bool TryParry(Component component, CombatParryData data, out ParryReceiver parryReceiver,
        out GameObject parriedGameObject)
    {
        return TryParry(component.gameObject, data, out parryReceiver, out parriedGameObject);
    }

    public static bool TryParry<T>(T[] components, CombatParryData data, out List<ParryReceiver> parryReceiver,
        out List<GameObject> parriedGameObjects)
        where T : Component
    {
        var hasParried = false;

        parryReceiver = new List<ParryReceiver>();
        parriedGameObjects = new List<GameObject>();

        foreach (var component in components)
            if (TryParry(component, data, out var parryable, out var parriedGameObject))
            {
                parryReceiver.Add(parryable);
                parriedGameObjects.Add(parriedGameObject);
                hasParried = true;
            }

        return hasParried;
    }
}

