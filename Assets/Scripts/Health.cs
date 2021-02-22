using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    [SerializeField] GameObject hitFXPrefab = default;
    [SerializeField] float hitFXDuration = 1f;
    [SerializeField] GameObject deathFXPrefab = default;
    [SerializeField] float deathFXDuration = 1f;
    [SerializeField] float deathAnimDuration = 1f;

    [SerializeField] public bool isInvulnerable = false;


    int health;
    int scorePoints = 0;
    public bool isAlive = true;

    AIStats stats;

    private void Awake()
    {
        stats = GetComponent<AIStats>();
    }

    private void Start()
    {
        if (gameObject.tag == "Player")
        {
            health = playerHealth;
        }
        else
        {
            health = stats.GetHealth();
            scorePoints = stats.GetScore();
        }
    }

    public void TakeDamage(int amount)
    {
        TriggerFX(hitFXPrefab, hitFXDuration);

        if (isInvulnerable) { return; }

        health -= amount;

        if (health <= 0f && isAlive)
        {
            Die();
        }
    }

    private void TriggerFX(GameObject fxPrefab, float fxDuration)
    {
        if (fxPrefab == null) { return; }

        GameObject newFX = Instantiate(fxPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(newFX, fxDuration);
    }

    public void TriggerHitFX()
    {
        TriggerFX(hitFXPrefab, hitFXDuration);
    }

    private void Die()
    {
        isAlive = false;

        if (gameObject.tag == "Enemy")
        {
            FindObjectOfType<LevelController>().RemoveEnemyFromList(GetComponent<AIBrain>());
            FindObjectOfType<LevelController>().IncrementEnemiesKilled();
            FindObjectOfType<ScoreManager>().AddToScore(scorePoints);
            PlaySFX("enemyDeath");
        }

        TriggerFX(deathFXPrefab, deathFXDuration);
        GetComponent<Animator>().SetTrigger("isDead");

        if (gameObject.tag == "Player")
        {
            // GetComponent<PlayerController>().enabled = false;
            FindObjectOfType<CanvasManager>().ShowGameOver();
            FindObjectOfType<LevelController>().DisableAllEnemyHealth();
            PlaySFX("playerDeath");
            StartCoroutine(DisablePlayerObject());
        }
        else
        {
            Destroy(gameObject, deathAnimDuration);
        }
    }

    private IEnumerator DisablePlayerObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void PlaySFX(string soundName)
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }

    public void GainHealth(int amount)
    {
        int maxHealth = stats.GetHealth();

        if ((health + amount) <= maxHealth)
        {
            health += amount;
        }
        else if (health < maxHealth)
        {
            health = maxHealth;
        }
    }

    public float GetFraction()
    {
        float maxHealth;

        if (gameObject.tag == "Player")
        {
            maxHealth = playerHealth;
        }
        else
        {
            maxHealth = stats.GetHealth();
        }

        return health / maxHealth;
    }
}
