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

    public void TakeDamage(float amount)
    {
        health -= amount;
        TriggerFX(hitFXPrefab, hitFXDuration);

        if (health <= 0f)
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
        TriggerFX(deathFXPrefab, deathFXDuration);
        Destroy(gameObject);
    }
}
