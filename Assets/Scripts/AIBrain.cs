using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBrain : MonoBehaviour
{
    [SerializeField] float mergeSpeed = 700f;

    [SerializeField] public GameObject target = default;

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
        if (target.activeSelf == false) { target = null; }

        Follow();
    }

    private void Follow()
    {
        if (target == null) { return; }
        if (target.transform.root.gameObject.tag == "Enemy") { mover.SetMaxForce(mergeSpeed); }
        else { mover.ResetMoveSpeed(); }

        mover.SetMoveTarget(target.transform.position);
    }


}