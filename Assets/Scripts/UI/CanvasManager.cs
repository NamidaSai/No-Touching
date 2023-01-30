using Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject HUDScreen = default;
        [SerializeField] private GameObject gameOverScreen = default;
        [SerializeField] private GameObject pauseMenu = default;

        public bool gamePaused = false;
        
        private AudioManager audioManager;

        private void Awake()
        {
            Time.timeScale = 1f;
            audioManager = FindObjectOfType<AudioManager>();
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
            audioManager.Play("gameOver");
        }

        private void ShowPauseMenu()
        {
            HUDScreen.SetActive(!gamePaused);
            pauseMenu.SetActive(gamePaused);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void GamePaused()
        {
            if (gamePaused)
            {
                Time.timeScale = 1f;
                gamePaused = false;
                audioManager.StopPauseSounds();
            }

            else
            {
                Time.timeScale = 0f;
                gamePaused = true;
            }

            ShowPauseMenu();

        }
    }
}