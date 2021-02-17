using UnityEngine;

public class AIDetection : MonoBehaviour
{
    [SerializeField] LayerMask targetLayers = default;

    AIBrain brain;

    private void Start()
    {
        brain = GetComponentInParent<AIBrain>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
        {
            brain.target = other.gameObject;
        }
    }
}