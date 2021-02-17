using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AIMerge : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer = default;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (targetLayer == (targetLayer | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.GetComponent<AIMerge>() == null) { return; }
            CreateJoint(other.gameObject);
        }
    }

    private void CreateJoint(GameObject other)
    {
        bool hasJoint = GetComponent<FixedJoint2D>() != null;

        if (hasJoint)
        {
            bool jointToOther = GetComponent<FixedJoint2D>().connectedBody == other.GetComponent<Rigidbody2D>();

            if (jointToOther) { return; }
        }


        bool otherHasJoint = other.GetComponent<FixedJoint2D>() != null;

        if (otherHasJoint)
        {
            bool otherIsAlreadyJoint = other.GetComponent<FixedJoint2D>().connectedBody == GetComponent<Rigidbody2D>();

            if (otherIsAlreadyJoint) { return; }
        }

        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = other.GetComponent<Rigidbody2D>();
    }
}