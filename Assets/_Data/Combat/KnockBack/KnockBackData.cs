using UnityEngine;

public class KnockBackData
{
    public Vector2 Angle;
    public int Direction;
    public float Strength;

    public KnockBackData(Vector2 angle, float strength, int direction, GameObject source)
    {
        Angle = angle;
        Strength = strength;
        Direction = direction;
        Source = source;
    }

    public GameObject Source { get; private set; }
}