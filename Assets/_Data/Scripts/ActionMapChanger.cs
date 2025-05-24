
using UnityEngine;

public class ActionMapChanger : NhoxBehaviour
{
    private void HandleGameStateChanged(GameManager.GameState state)
    {
        InputManager.Instance.SetUIMode(state == GameManager.GameState.UI);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }
}
