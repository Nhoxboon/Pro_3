
using System.Collections;
using UnityEngine;

public class UI : NhoxBehaviour
{
    [SerializeField] protected FadeScreen fadeScreen;
    [SerializeField] protected Transform endText;
    [SerializeField] protected RestartGameBtn restartGameBtn;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFadeScreen();
        LoadEndText();
        LoadRestartGameBtn();
    }
    
    protected void LoadFadeScreen()
    {
        if (fadeScreen != null) return;
        fadeScreen = GetComponentInChildren<FadeScreen>();
        Debug.Log(transform.name + " :LoadFadeScreen", gameObject);
    }

    protected void LoadEndText()
    {
        if (endText != null) return;
        endText = transform.Find("CenterUI/DeadScreen/YouDie!-Text");
        Debug.Log(transform.name + " :LoadEndText", gameObject);
    }
    
    protected void LoadRestartGameBtn()
    {
        if (restartGameBtn != null) return;
        restartGameBtn = GetComponentInChildren<RestartGameBtn>();
        Debug.Log(transform.name + " :LoadRestartGameBtn", gameObject);
    }

    public void SwitchToEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }
    
    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        endText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartGameBtn.gameObject.SetActive(true);
    }
}