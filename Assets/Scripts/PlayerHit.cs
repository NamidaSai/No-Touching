using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] float damageOnHit = 10f;
    [SerializeField] LayerMask targetLayers = default;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
        {
            float thisDamage = damageOnHit;

            if (other.gameObject.GetComponent<AIHitter>() != null)
            {
                thisDamage = other.gameObject.GetComponent<AIHitter>().GetJointDamage();
                other.gameObject.GetComponent<AIHitter>().HitStunned();
            }

            GetComponent<Health>().TakeDamage(thisDamage);
        }
    }
}