using UnityEngine;

[CreateAssetMenu(fileName = "EntityAudioDataSO", menuName = "ScriptableObject/Enity Data/Audio Data")]
public class EntityAudioDataSO : ScriptableObject
{
    public AudioClip moveClip;
    public AudioClip hitClip;
    public AudioClip deathClip;
}