using UnityEngine;

public class ParryData
{
    public ParryData(GameObject source)
    {
        Source = source;
    }

    public GameObject Source { get; private set; }
}