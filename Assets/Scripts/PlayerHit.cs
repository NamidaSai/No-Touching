using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] float damageOnHit = 10f;
    [SerializeField] LayerMask targetLayers = default;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
        {
            GetComponent<Health>().TakeDamage(damageOnHit);
        }
    }
}