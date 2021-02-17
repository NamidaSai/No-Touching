using UnityEngine;

public class AIMerge : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer = default;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AIMerge>() == null) { return; }
        if (other.gameObject.transform == transform) { return; }
        if (other.GetComponentInParent<AIBrain>().isMerged) { return; }

        if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            other.GetComponentInParent<AIBrain>().AddToMergedObjects(transform.root.gameObject);
            transform.root.parent = other.gameObject.transform.root;
            GetComponentInParent<AIBrain>().ResolveMerge(true);
        }
    }
}