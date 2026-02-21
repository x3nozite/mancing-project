using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentState;

    public void updateState(GameState state)
    {
        currentState = state;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
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