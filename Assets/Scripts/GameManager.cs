using TMPro;
using UnityEngine;

public enum GameState
{
    INGAME,
    PAUSED,
}
public class GameManager : MonoBehaviour
{

    private GameState gameState;
    public static GameManager instance;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI scoreText;
    public int stage;
    public int score;

    public GameState GameState { get => gameState; set => gameState = value; }

    private void Awake()
    {
        instance = this;
        gameState = GameState.INGAME;
        
    }

    private void Update()
    {
        PauseGame();
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverUI.activeInHierarchy)
        {
            if (!pauseUI.activeInHierarchy)
            {
                pauseUI.SetActive(true);
                gameState = GameState.PAUSED;
                SetTimeScale(0);
            }
            else
            {
                pauseUI.SetActive(false);

                gameState = GameState.INGAME;
                SetTimeScale(1);
            }
        }
    }
    public void SetTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }
    public void PauseGameState()
    {
        this.gameState =GameState.PAUSED; 
    }
    public void IngameGameState()
    {
        this.gameState = GameState.INGAME;
    }
    public void SetScoreText()
    {
        gameOverUI.SetActive(true);
        scoreText.text = $"Your Score: {score}";
    }

}
