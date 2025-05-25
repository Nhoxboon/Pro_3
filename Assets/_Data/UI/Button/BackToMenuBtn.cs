
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuBtn : BtnBaseAbstract
{
    protected override void OnClick()
    {
        BackToMenu();
    }

    protected void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
