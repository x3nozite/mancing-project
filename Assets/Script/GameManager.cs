using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    public void updateState(GameState state)
    {
        currentState = state;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    Paused,
    GameOver
}