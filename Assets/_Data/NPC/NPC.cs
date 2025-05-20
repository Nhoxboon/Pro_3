using System.Collections;
using UnityEngine;

public class NPC : NhoxBehaviour, IInteractable
{
    [SerializeField] protected NPCDialogueSO dialogueData;

    protected int dialogueIndex;
    protected bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null) return;

        if (isDialogueActive)
            NextLine();
        else
            StartDialogue();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadNPCDialogueSO();
    }

    protected void LoadNPCDialogueSO()
    {
        if (dialogueData != null) return;
        dialogueData = Resources.Load<NPCDialogueSO>("NPC/" + transform.name);
        Debug.Log(transform.name + " LoadNPCDialogueSO", gameObject);
    }

    protected void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        DialogueManager.Instance.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);

        DialogueManager.Instance.ShowDialogueUI(true);
        InputManager.Instance.Pause();

        StartCoroutine(TypeLine());
    }

    protected void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            DialogueManager.Instance.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    protected IEnumerator TypeLine()
    {
        isTyping = true;
        DialogueManager.Instance.SetDialogueText(string.Empty);
        foreach (var letter in dialogueData.dialogueLines[dialogueIndex])
        {
            DialogueManager.Instance.SetDialogueText(DialogueManager.Instance.dialogueText.text += letter);
            AudioManager.Instance.PlaySFX(dialogueData.voiceSound, dialogueData.voicePitch);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        DialogueManager.Instance.SetDialogueText(string.Empty);

        DialogueManager.Instance.ShowDialogueUI(false);
        InputManager.Instance.Unpause();
    }
}