using Level;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private int maxScore = 999999;
        private TextMeshProUGUI scoreText;
        private ScoreManager scoreManager;

        private void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        private void Update()
        {
            int currentScore = scoreManager.GetScore();
            int scoreToDisplay = maxScore;

            if (currentScore < maxScore)
            {
                scoreToDisplay = currentScore;
            }

            scoreText.text = scoreToDisplay.ToString();
        }
    }
}