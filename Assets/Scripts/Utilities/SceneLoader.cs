using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float transitionDelay = 1f;
        [SerializeField] private GameObject fader = default;

        private int currentSceneIndex;

        private void Awake()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            fader.SetActive(true);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(transitionDelay);
            if (currentSceneIndex == 0)
            {
                LoadNextScene();
            }
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            StartCoroutine(LoadSceneWithTransition(1));
        }

        public void LoadGame()
        {
            StartCoroutine(LoadSceneWithTransition(2));
        }

        public void LoadNextScene()
        {
            StartCoroutine(LoadSceneWithTransition(currentSceneIndex + 1));
        }

        private IEnumerator LoadSceneWithTransition(int targetSceneIndex)
        {
            fader.GetComponent<Animator>().SetTrigger("FadeOut");
            yield return new WaitForSeconds(transitionDelay);
            SceneManager.LoadScene(targetSceneIndex);
        }

        public void ResetScene()
        {
            StartCoroutine(LoadSceneWithTransition(currentSceneIndex));
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}