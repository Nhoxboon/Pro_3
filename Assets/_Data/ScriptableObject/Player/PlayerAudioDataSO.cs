using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAudioDataSO", menuName = "ScriptableObject/Player Data/Audio Data")]
public class PlayerAudioDataSO : EntityAudioDataSO
{
    public AudioClip climbAudio;
    public AudioClip wallSlideAudio;
    public AudioClip jumpAudio;
    public AudioClip landAudio;
    public AudioClip dashAudio;
}
