using AI;
using Audio;
using Cinemachine;
using UnityEngine;

namespace Combat
{
    public class PlayerHit : MonoBehaviour
    {
        [SerializeField] int damageOnHit = 10;
        [SerializeField] LayerMask targetLayers = default;
        
        private Health health;
        private CinemachineImpulseSource cinemachineImpulseSource;
        private AudioManager audioManager;

        public void Awake()
        {
            health = GetComponent<Health>();
            cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
            {
                int thisDamage = damageOnHit;

                if (!health.isAlive) { return; }

                AIHitter aiHitter = other.gameObject.GetComponent<AIHitter>();
                if (aiHitter != null)
                {
                    thisDamage = aiHitter.GetDamage();
                    aiHitter.HitStunned();
                }

                health.TakeDamage(thisDamage);
                TriggerHitFX();
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                PlaySFX("playerWall");
            }
        }

        public void TriggerHitFX()
        {
            if (!health.isAlive) { return; }

            cinemachineImpulseSource.GenerateImpulse();
            PlaySFX("playerHit");
        }

        private void PlaySFX(string soundName)
        {
            audioManager.Play(soundName);
        }
    }
}