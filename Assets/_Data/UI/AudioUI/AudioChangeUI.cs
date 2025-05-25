
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioChangeUI : NhoxBehaviour
{
    [SerializeField] protected Image panel;
    [SerializeField] protected Button audioBtn;
    [SerializeField] protected Button backToMenuBtn;
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
        backToMenuBtn?.gameObject.SetActive(isUI);

        GameManager.Instance.ChangeState(isUI ? GameManager.GameState.UI : GameManager.GameState.Gameplay);
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanel();
        LoadAudioBtn();
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
