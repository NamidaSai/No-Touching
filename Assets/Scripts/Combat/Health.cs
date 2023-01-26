using System.Collections;
using AI;
using Audio;
using Level;
using UI;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;
        [SerializeField] int scorePoints = 0;
        [SerializeField] GameObject hitFXPrefab = default;
        [SerializeField] float hitFXDuration = 1f;
        [SerializeField] GameObject deathFXPrefab = default;
        [SerializeField] float deathFXDuration = 1f;
        [SerializeField] float deathAnimDuration = 1f;

        [SerializeField] public bool isInvulnerable = false;

        float health;
        public bool isAlive = true;

        private void Awake()
        {
            health = maxHealth;
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

        public void TriggerHitFX()
        {
            TriggerFX(hitFXPrefab, hitFXDuration);
        }

        private void Die()
        {
            isAlive = false;

            if (gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                FindObjectOfType<LevelController>().RemoveEnemyFromList(GetComponent<AIBrain>());
                FindObjectOfType<ScoreManager>().AddToScore(scorePoints);
                PlaySFX("enemyDeath");
            }

            TriggerFX(deathFXPrefab, deathFXDuration);
            GetComponent<Animator>().SetTrigger("isDead");

            if (gameObject.tag == "Player")
            {
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

        public float GetFraction()
        {
            return health / maxHealth;
        }
    }
}
