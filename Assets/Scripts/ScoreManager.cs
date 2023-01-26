using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int currentScore = 0;

    ScoreDisplay display;

    private void Start()
    {
        display = FindObjectOfType<ScoreDisplay>();
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void AddToScore(int scoreValue)
    {
        currentScore += scoreValue;
        FindObjectOfType<AudioManager>().Play("scorePop");
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