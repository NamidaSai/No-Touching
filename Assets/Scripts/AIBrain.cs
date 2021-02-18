using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBrain : MonoBehaviour
{
    [SerializeField] float mergeForce = 20f;
    [SerializeField] float wanderCountdown = 2f;
    [SerializeField] bool followPlayerOnStart = false;

    [SerializeField] public GameObject target = default;
    [SerializeField] public List<GameObject> potentialTargets = new List<GameObject>();

    AIMover mover;

    float nextWander = 0f;

    private void Awake()
    {
        mover = GetComponent<AIMover>();
    }
    private void Start()
    {
        FindObjectOfType<LevelController>().AddEnemyToList(this);

        if (followPlayerOnStart)
        {
            target = FindObjectOfType<PlayerController>().gameObject;
        }
    }

    private void Update()
    {
        AllocateTarget();
        Follow();
    }

    private void AllocateTarget()
    {
        if (potentialTargets.Count == 0) { return; }

        GameObject closestTarget = GetClosestTarget();

        foreach (GameObject potentialTarget in potentialTargets)
        {
            if (potentialTarget == null) { continue; }

            if (potentialTarget.tag == "Enemy")
            {
                bool targetIsMerged = potentialTarget.GetComponent<AIMerge>().isMerged;

                if (!targetIsMerged)
                {
                    target = potentialTarget;
                    return;
                }
                else if (potentialTarget == closestTarget)
                {
                    if (FindObjectOfType<PlayerController>() == null) { continue; }

                    target = FindObjectOfType<PlayerController>().gameObject;
                    return;
                }
            }
        }

        target = closestTarget;
    }

    private GameObject GetClosestTarget()
    {
        GameObject closestTarget = null;

        foreach (GameObject potentialTarget in potentialTargets)
        {
            if (potentialTarget == null) { continue; }

            if (closestTarget == null)
            {
                closestTarget = potentialTarget;
                continue;
            }

            float distanceToTarget = Vector2.Distance(transform.position, potentialTarget.transform.position);
            float closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);

            if (distanceToTarget < closestDistance)
            {
                closestTarget = potentialTarget;
            }
        }

        return closestTarget;
    }

    private void Follow()
    {
        if (target == null) { return; }

        if (target.transform.root.gameObject.tag == "Enemy" && !GetComponent<AIBreaker>().isStunned)
        {
            mover.SetMaxForce(mergeForce);
        }

        else { mover.ResetMoveSpeed(); }

        mover.SetMoveTarget(target.transform.position);
    }

    private void Wander()
    {
        if (nextWander <= wanderCountdown)
        {
            nextWander += Time.deltaTime;
        }
        else
        {
            float force = 1000f;
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 destination = direction * force;
            GetComponent<Rigidbody2D>().AddForce(destination);
            nextWander = 0f;
        }
    }
}