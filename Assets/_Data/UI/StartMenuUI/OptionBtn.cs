
using UnityEngine;
using UnityEngine.UI;

public class OptionBtn : BtnBaseAbstract
{
    [SerializeField] protected Transform menuBtn;
    [SerializeField] protected Image audioPanel;
    protected override void OnClick()
    {
        TurnOnAudioPanel();
        // TurnOffMenuBtn();
    }
    
    protected void TurnOnAudioPanel()
    {
        audioPanel.gameObject.SetActive(true);
    }
    
    protected void TurnOffMenuBtn()
    {
        menuBtn.gameObject.SetActive(false);
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMenuBtn();
        LoadAudioPanel();
    }

    protected void LoadMenuBtn()
    {
        if (menuBtn != null) return;
        menuBtn = GetComponentInParent<Transform>();
        Debug.Log(transform.name + " :LoadMenuBtn", gameObject);
    }

    protected void LoadAudioPanel()
    {
        if (audioPanel != null) return;

        foreach (Transform child in transform)
        {
            Image img = child.GetComponent<Image>();
            if (img != null)
            {
                audioPanel = img;
                break;
            }
        }
        Debug.Log(transform.name + " :LoadPanel", gameObject);
    }

}
    