using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBrain : MonoBehaviour
{
    [SerializeField] float maxBreakSpeed = 10f;
    [SerializeField] float maxBreakForce = 10f;
    [SerializeField] public GameObject target = default;
    [SerializeField] GameObject[] mergeSubObjects = default;

    [SerializeField] List<GameObject> mergedObjects = new List<GameObject>();


    public bool isMerged = false;
    AIMover mover;

    private void Start()
    {
        mover = GetComponent<AIMover>();

        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }
    }

    private void Update()
    {
        if (target == null) { return; }

        Follow();
    }

    private void Follow()
    {
        mover.SetMoveTarget(target.transform.position);
    }

    public void ResolveMerge(bool merged)
    {
        isMerged = merged;

        GetComponent<CircleCollider2D>().enabled = !merged;
        GetComponent<Rigidbody2D>().isKinematic = merged;
        target = null;

        foreach (GameObject subObject in mergeSubObjects)
        {
            subObject.SetActive(!merged);
        }
    }

    public void AddToMergedObjects(GameObject newObject)
    {
        mergedObjects.Add(newObject);
    }

    public void UnmergeObjects()
    {
        foreach (GameObject mergedObject in mergedObjects)
        {
            mergedObject.transform.parent = null;
            StartCoroutine(AddBreakingForce(mergedObject));
        }

        mergedObjects.Clear();
    }

    private IEnumerator AddBreakingForce(GameObject other)
    {
        Vector2 direction = (other.transform.position - transform.position).normalized;
        Vector2 steer = direction - other.GetComponent<Rigidbody2D>().velocity;
        steer *= maxBreakSpeed;
        steer = Vector2.ClampMagnitude(steer, maxBreakForce);
        other.GetComponent<Rigidbody2D>().velocity = steer;

        yield return new WaitForSeconds(0.1f);

        other.GetComponent<AIBrain>().ResolveMerge(false);
    }
}