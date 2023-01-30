using Audio;
using UI;
using UnityEngine;

namespace Level
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int currentScore = 0;

        private ScoreDisplay display;
        private AudioManager audioManager;

        private void Awake()
        {
            display = FindObjectOfType<ScoreDisplay>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        public int GetScore()
        {
            return currentScore;
        }

        public void AddToScore(int scoreValue)
        {
            currentScore += scoreValue;
            audioManager.Play("scorePop");
            TriggerAnimation();
        }

        public void ResetGame()
        {
            Destroy(gameObject);
        }

        private void TriggerAnimation()
        {
            display.GetComponent<Animator>().SetTrigger("Scored");
        }
    }
}