using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "ScriptableObject/NPC Data/Dialogue Data")]
public class NPCDialogueSO : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public float autoProgressDelay = 1.5f;

    public DialogueChoice[] choices;
}