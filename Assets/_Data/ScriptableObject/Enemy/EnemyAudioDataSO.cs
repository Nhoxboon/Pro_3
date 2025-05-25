using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAudioDataSO", menuName = "ScriptableObject/Enemy Data/Audio Data")]
public class EnemyAudioDataSO : EntityAudioDataSO
{
    public AudioClip jumpClip;
    public AudioClip meleeAttackClip;
    public AudioClip rangedAttackClip;
}