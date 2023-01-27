using Level;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] int maxScore = 999999;
        TextMeshProUGUI scoreText;
        ScoreManager scoreManager;

        void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        void Update()
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