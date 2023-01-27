using System.Collections;
using AI;
using Audio;
using Level;
using Player;
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
        
        private LevelController levelController;
        private ScoreManager scoreManager;
        private CanvasManager canvasManager;
        private Collider2D thisCollider;
        private AudioManager audioManager;
        private Animator animator;
        private AIBrain aiBrain;

        private void Awake()
        {
            health = maxHealth;
            
            levelController = FindObjectOfType<LevelController>();
            scoreManager = FindObjectOfType<ScoreManager>();
            canvasManager = FindObjectOfType<CanvasManager>();
            audioManager = FindObjectOfType<AudioManager>();
            
            thisCollider = gameObject.GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            aiBrain = GetComponent<AIBrain>();
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

            if (gameObject.CompareTag("Enemy"))
            {
                thisCollider.enabled = false;
                levelController.RemoveEnemyFromList(aiBrain);
                scoreManager.AddToScore(scorePoints);
                PlaySFX("enemyDeath");
            }

            TriggerFX(deathFXPrefab, deathFXDuration);
            animator.SetTrigger("isDead");

            if (gameObject.CompareTag("Player"))
            {
                canvasManager.ShowGameOver();
                levelController.DisableAllEnemyHealth();
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
            gameObject.GetComponent<Shooter>().enabled = false;
            gameObject.GetComponent<PlayerMover>().Disable();
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }

        private void PlaySFX(string soundName)
        {
            audioManager.Play(soundName);
        }

        public float GetFraction()
        {
            return health / maxHealth;
        }
    }
}
