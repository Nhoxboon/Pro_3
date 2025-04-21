
using UnityEngine;

[CreateAssetMenu(fileName = "EntityAudioDataSO", menuName = "ScriptableObject/Enity Data/Audio Data")]
public class EntityAudioDataSO : ScriptableObject
{
    public AudioClip moveAudio;
    public AudioClip climbAudio;
    public AudioClip jumpAudio;
    public AudioClip fallAudio;
    public AudioClip hitAudio;
}
