using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject HUDScreen = default;
    [SerializeField] GameObject gameOverScreen = default;
    [SerializeField] GameObject pauseMenu = default;

    public bool gamePaused = false;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        HUDScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowGameOver()
    {
        HUDScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    private void ShowPauseMenu()
    {
        HUDScreen.SetActive(!gamePaused);
        pauseMenu.SetActive(gamePaused);
    }

    public void GamePaused()
    {
        if (gamePaused)
        {
            Time.timeScale = 1f;
            gamePaused = false;
            FindObjectOfType<AudioManager>().StopPauseSounds();
        }

        else
        {
            Time.timeScale = 0f;
            gamePaused = true;
        }

        ShowPauseMenu();

    }
}