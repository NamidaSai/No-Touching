using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] GameObject hitFXPrefab = default;
    [SerializeField] float hitFXDuration = 1f;
    [SerializeField] GameObject deathFXPrefab = default;
    [SerializeField] float deathFXDuration = 1f;

    [SerializeField] bool isInvulnerable = false;

    bool isAlive = true;

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
        }

        TriggerFX(deathFXPrefab, deathFXDuration);

        if (gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
