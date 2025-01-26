using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public enum GameState
    {
        MainMenu,
        InGame,
        GameOver
    }

    private GameState _currentState;

    private void Awake()
    {
        CreateSingleton();
    }

    public void ChangeState(GameState newState)
    {
        _currentState = newState;
        Debug.Log("Game State: " + _currentState);
    }

    private void CreateSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
