
using UnityEngine;

public class RestartGameBtn : BtnBaseAbstract
{
    protected override void OnClick()
    {
        RestartGame();
    }

    protected void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}
