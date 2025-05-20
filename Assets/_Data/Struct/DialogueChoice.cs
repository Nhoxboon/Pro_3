using System;

[Serializable]
public struct DialogueChoice
{
    public int dialogueIndex;
    public string[] choices;
    public int[] nextDialogueIndexes;
}