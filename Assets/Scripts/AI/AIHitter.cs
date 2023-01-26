using Player;
using UnityEngine;

namespace AI
{
    public class AIHitter : MonoBehaviour
    {
        [SerializeField] float attackPushForce = 1000f;

        [SerializeField] int damage = 100;

        public int GetDamage()
        {
            return damage;
        }

        public void HitStunned()
        {
            Vector2 playerPosition = FindObjectOfType<PlayerController>().transform.position;

            Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
            Vector2 pushForce = direction * attackPushForce;
            GetComponent<Rigidbody2D>().AddForce(pushForce);
        }
    }
}