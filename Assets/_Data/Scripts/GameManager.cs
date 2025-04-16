using System;
using UnityEngine;

public class GameManager : NhoxBehaviour
{
    public enum GameState
    {
        UI,
        Gameplay
    }

    private GameState currentGameState = GameState.Gameplay;
    public event Action<GameState> OnGameStateChanged;

    public void ChangeState(GameState state)
    {
        if (state == currentGameState)
            return;

        switch (state)
        {
            case GameState.UI:
                EnterUIState();
                break;
            case GameState.Gameplay:
                EnterGameplayState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    private void EnterUIState()
    {
        Time.timeScale = 0f;
    }

    private void EnterGameplayState()
    {
        Time.timeScale = 1f;
    }
}