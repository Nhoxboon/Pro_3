
using UnityEngine;

public class ActionMapChanger : NhoxBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void HandleGameStateChanged(GameManager.GameState state)
    {
        InputManager.Instance.SetUIMode(state == GameManager.GameState.UI);
    }

    private void OnEnable()
    {
        gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        gameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGameManager();
    }
    
    protected void LoadGameManager()
    {
        if (gameManager != null) return;
        gameManager = FindFirstObjectByType<GameManager>();
        Debug.Log(transform.name + " :LoadGameManager", gameObject);
    }
}
