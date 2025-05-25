
using System.Collections;
using UnityEngine;

public class UI : NhoxBehaviour
{
    [SerializeField] protected FadeScreen fadeScreen;
    [SerializeField] protected Transform endgameText;
    [SerializeField] protected Transform endText;
    [SerializeField] protected RestartGameBtn restartGameBtn;
    [SerializeField] protected BackToMenuBtn backToMenuBtn;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFadeScreen();
        LoadEndGameText();
        LoadEndText();
        LoadRestartGameBtn();
        LoadBackToMenuBtn();
    }
    
    protected void LoadFadeScreen()
    {
        if (fadeScreen != null) return;
        fadeScreen = GetComponentInChildren<FadeScreen>();
        Debug.Log(transform.name + " :LoadFadeScreen", gameObject);
    }

    protected void LoadEndGameText()
    {
        if (endText != null) return;
        endgameText = transform.Find("CenterUI/EndScreen/YouWin!-Text");
        Debug.Log(transform.name + " :LoadEndGameText", gameObject);
    }

    protected void LoadEndText()
    {
        if (endText != null) return;
        endText = transform.Find("CenterUI/EndScreen/YouDie!-Text");
        Debug.Log(transform.name + " :LoadEndText", gameObject);
    }
    
    protected void LoadRestartGameBtn()
    {
        if (restartGameBtn != null) return;
        restartGameBtn = GetComponentInChildren<RestartGameBtn>();
        Debug.Log(transform.name + " :LoadRestartGameBtn", gameObject);
    }
    
    protected void LoadBackToMenuBtn()
    {
        if (backToMenuBtn != null) return;
        backToMenuBtn = GetComponentInChildren<BackToMenuBtn>();
        Debug.Log(transform.name + " :LoadBackToMenuBtn", gameObject);
    }

    public void SwitchToDeadScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(DeadScreenCoroutine());
    }
    
    public void SwitchToEndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(1.5f);
        endgameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        backToMenuBtn.gameObject.SetActive(true);
        GameManager.Instance.ChangeState(GameManager.GameState.UI);
    }
    
    IEnumerator DeadScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        endText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartGameBtn.gameObject.SetActive(true);
    }
}