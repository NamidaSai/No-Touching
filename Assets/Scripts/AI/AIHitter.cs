using Player;
using UnityEngine;

namespace AI
{
    public class AIHitter : MonoBehaviour
    {
        [SerializeField] float attackPushForce = 1000f;
        [SerializeField] int damage = 100;
        
        private PlayerController player;
        private Rigidbody2D thisRigidbody;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
            thisRigidbody = GetComponent<Rigidbody2D>();
        }

        public int GetDamage()
        {
            return damage;
        }

        public void HitStunned()
        {
            Vector2 playerPosition = player.transform.position;

            Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
            Vector2 pushForce = direction * attackPushForce;
            thisRigidbody.AddForce(pushForce);
        }
    }
}