using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] int scorePoints = 25;
    [SerializeField] GameObject hitFXPrefab = default;
    [SerializeField] float hitFXDuration = 1f;
    [SerializeField] GameObject deathFXPrefab = default;
    [SerializeField] float deathFXDuration = 1f;

    [SerializeField] bool isInvulnerable = false;

    float maxHealth;

    bool isAlive = true;

    private void Start()
    {
        maxHealth = health;
    }

    public void TakeDamage(float amount)
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

    private void Die()
    {
        isAlive = false;

        if (gameObject.tag == "Enemy")
        {
            FindObjectOfType<LevelController>().RemoveEnemyFromList(GetComponent<AIBrain>());
            FindObjectOfType<LevelController>().IncrementEnemiesKilled();
            FindObjectOfType<ScoreManager>().AddToScore(scorePoints);
        }

        TriggerFX(deathFXPrefab, deathFXDuration);

        if (gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            FindObjectOfType<CanvasManager>().ShowGameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GainHealth(float amount)
    {
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
        return health / maxHealth;
    }
}
