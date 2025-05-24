using UnityEngine;

public class ExitBtn : BtnBaseAbstract
{
    protected override void OnClick()
    {
        QuitGame();
    }

    protected void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}