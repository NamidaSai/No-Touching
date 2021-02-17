using UnityEngine;

public class AIBreaker : MonoBehaviour
{
    [SerializeField] LayerMask targetLayers = default;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
        {
            GetComponent<AIBrain>().UnmergeObjects();
        }
    }
}