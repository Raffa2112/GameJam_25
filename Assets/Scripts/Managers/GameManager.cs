using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _gameOverCanvas;

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
        Application.targetFrameRate = 120;
    }

    private void ChangeState(GameState newState)
    {
        _currentState = newState;
    }

    public void OnGameOver()
    {
        ChangeState(GameState.GameOver);
        StartCoroutine(OpenGameOverMenu());
    }

    private IEnumerator OpenGameOverMenu()
    {
        yield return new WaitForSeconds(0.5f);
        _gameOverCanvas.gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        ChangeState(GameState.InGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
