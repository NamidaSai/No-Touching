using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merger : MonoBehaviour
{
    public bool isMerged = false;
    public bool isMerging = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isMerging = true;
            ResolveMerge(other.gameObject.GetComponent<Merger>());
        }
    }

    private void ResolveMerge(Merger otherMerger)
    {
        if (!otherMerger.isMerged)
        {

        }
    }
}
