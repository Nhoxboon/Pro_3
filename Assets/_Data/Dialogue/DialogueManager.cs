
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : NhoxBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;
    
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    
    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 DialogueManager allow to exist");
            return;
        }
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanel();
        LoadDialogueText();
        LoadNameText();
        LoadPortraitImage();
    }

    protected void LoadPanel()
    {
        if (dialoguePanel != null) return;
        dialoguePanel = GameObject.Find("UI/BotCenterUI/DialoguePanel");
        Debug.Log(transform.name + " LoadPanel", gameObject);
    }
    
    protected void LoadDialogueText()
    {
        if (dialogueText != null) return;
        dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<TMP_Text>();
        Debug.Log(transform.name + " LoadDialogueText", gameObject);
    }
    
    protected void LoadNameText()
    {
        if (nameText != null) return;
        nameText = dialoguePanel.transform.Find("NPCNameText").GetComponent<TMP_Text>();
        Debug.Log(transform.name + " LoadNameText", gameObject);
    }
    
    protected void LoadPortraitImage()
    {
        if (portraitImage != null) return;
        portraitImage = dialoguePanel.transform.Find("DialoguePortrait").GetComponent<Image>();
        Debug.Log(transform.name + " LoadPortraitImage", gameObject);
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }
    
    public void SetNPCInfo(string npcName, Sprite portrait)
    {
        nameText.SetText(npcName);
        portraitImage.sprite = portrait;
    }
    
    public void SetDialogueText(string text)
    {
        dialogueText.SetText(text);
    }
}
