
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : BtnMenuAbstract
{
    protected override void OnClick()
    {
        LoadScene();
    }
    
    protected void LoadScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
