
using System;
using UnityEngine;

[Serializable]
public class AttackOptionalSprite : AttackData
{
    [field: SerializeField] public bool UseOptionalSprite { get; private set; }
    public Sprite sprite;
}
