﻿using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "ScriptableObject/NPC Data/Dialogue Data")]
public class NPCDialogueSO : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
}
