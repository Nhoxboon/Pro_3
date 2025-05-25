
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : BtnBaseAbstract
{
    [SerializeField] protected FadeScreen fadeScreen;
    
    protected override void OnClick()
    {
        StartCoroutine(LoadSceneWithDelay(1.1f));
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFadeScreen();
    }
    
    protected void LoadFadeScreen()
    {
        if (fadeScreen != null) return;
        fadeScreen = FindObjectOfType<FadeScreen>();
        Debug.Log(transform.name + " :LoadFadeScreen", gameObject);
    }

    IEnumerator LoadSceneWithDelay(float delay)
    {
        fadeScreen.FadeOut();
        
        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadSceneAsync(1);
    }
}
