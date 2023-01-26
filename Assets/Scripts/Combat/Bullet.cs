using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float lifetime = 2f;
        [SerializeField] private float damageAmount = 2f;

        private float timeAlive = 0f;

        private void Update()
        {
            if (timeAlive < lifetime)
            {
                timeAlive += Time.deltaTime;
            }
            else
            {
                Vanish();
            }
        }

        private void Vanish()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Health target = collider.GetComponent<Health>();
            
            if (target != null)
            {
                target.TakeDamage(damageAmount);
            }
            
            Vanish();
        }
    }
}