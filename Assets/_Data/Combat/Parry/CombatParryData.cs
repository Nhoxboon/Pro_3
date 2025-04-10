using UnityEngine;

public class CombatParryData
{
    public CombatParryData(GameObject source)
    {
        Source = source;
    }

    public GameObject Source { get; private set; }
}