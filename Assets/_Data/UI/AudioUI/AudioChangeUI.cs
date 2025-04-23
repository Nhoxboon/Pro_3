
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioChangeUI : NhoxBehaviour
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected Image panel;
    [SerializeField] protected Button audioBtn;
    [SerializeField] protected bool isUI;

    protected void OnEnable()
    {
        audioBtn.onClick.AddListener(HandleAudioBtnClick);
    }
    
    protected void OnDisable()
    {
        audioBtn.onClick.RemoveListener(HandleAudioBtnClick);
    }

    protected void HandleAudioBtnClick()
    {
        isUI = !panel.gameObject.activeSelf;
        panel.gameObject.SetActive(isUI);

        gameManager.ChangeState(isUI ? GameManager.GameState.UI : GameManager.GameState.Gameplay);
        
        if (!isUI)
            EventSystem.current.SetSelectedGameObject(null);
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGameManager();
        LoadPanel();
        LoadAudioBtn();
    }
    
    protected void LoadGameManager()
    {
        if (gameManager != null) return;
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log(transform.name + " :LoadGameManager", gameObject);
    }
    
    protected void LoadPanel()
    {
        if (panel != null) return;
        panel = GetComponentInChildren<Image>();
        Debug.Log(transform.name + " :LoadPanel", gameObject);
    }
    
    protected void LoadAudioBtn()
    {
        if (audioBtn != null) return;
        audioBtn = GetComponentInChildren<Button>();
        Debug.Log(transform.name + " :LoadAudioBtn", gameObject);
    }
}
