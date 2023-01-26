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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
            {
                int thisDamage = damageOnHit;

                if (!GetComponent<Health>().isAlive) { return; }

                if (other.gameObject.GetComponent<AIHitter>() != null)
                {
                    thisDamage = other.gameObject.GetComponent<AIHitter>().GetJointDamage();
                    other.gameObject.GetComponent<AIHitter>().HitStunned();
                }

                GetComponent<Health>().TakeDamage(thisDamage);
                TriggerHitFX();
            }
            else if (other.gameObject.tag == "Wall")
            {
                PlaySFX("playerWall");
            }
        }

        public void TriggerHitFX()
        {
            if (!GetComponent<Health>().isAlive) { return; }

            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            PlaySFX("playerHit");
        }

        private void PlaySFX(string soundName)
        {
            FindObjectOfType<AudioManager>().Play(soundName);
        }
    }
}