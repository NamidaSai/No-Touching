using UnityEngine;

public class AIBreaker : MonoBehaviour
{
    [SerializeField] float maxBreakSpeed = 10f;
    [SerializeField] float maxBreakForce = 10f;
    [SerializeField] LayerMask targetLayers = default;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (targetLayers == (targetLayers | (1 << other.gameObject.layer)))
        {
            BreakAllJoints();
        }
    }

    private void AddBreakingForce(Vector2 jointPosSum, Vector2 collisionPos)
    {
        Vector2 directionFromOther = (jointPosSum - (Vector2)transform.position).normalized;
        Vector2 directionFromCollision = (collisionPos - (Vector2)transform.position).normalized;
        Vector2 sumDirection = directionFromOther + directionFromCollision;

        Vector2 steer = sumDirection - GetComponent<Rigidbody2D>().velocity;
        steer *= maxBreakSpeed;
        steer = Vector2.ClampMagnitude(steer, maxBreakForce);
        GetComponent<Rigidbody2D>().AddForce(steer);
    }

    private void BreakAllJoints()
    {
        Vector2 sumDirection = new Vector2();
        int directionCount = 0;

        FixedJoint2D[] joints = GetComponents<FixedJoint2D>();

        foreach (FixedJoint2D joint in joints)
        {
            float distance = Vector2.Distance(transform.position, joint.connectedBody.gameObject.transform.position);

            if (distance > 0)
            {
                Vector2 direction = (transform.position - joint.connectedBody.gameObject.transform.position).normalized;
                Vector2 weightedDirection = direction / distance;
                sumDirection += weightedDirection;
                directionCount++;
            }

            Destroy(joint);
        }

        if (directionCount > 0)
        {
            sumDirection /= directionCount;
            sumDirection *= maxBreakSpeed;
            Vector2 steer = sumDirection - GetComponent<Rigidbody2D>().velocity;
            steer = Vector2.ClampMagnitude(steer, maxBreakForce);
            GetComponent<Rigidbody2D>().AddForce(steer);
        }
    }
}