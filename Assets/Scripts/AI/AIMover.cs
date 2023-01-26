using Level;
using Pathfinding;
using UnityEngine;

namespace AI
{
    public class AIMover : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float slowingRadius = 0.3f;

        [Header("Pathfinding")]
        [SerializeField] float nextWaypointDistance = 3f;

        Vector2 moveTarget;

        Path path;
        int currentWaypoint = 0;
        float maxForce;

        Seeker seeker;
        Rigidbody2D thisRigidbody;
        LevelController levelController;
        AIStats stats;

        private void Awake()
        {
            seeker = GetComponent<Seeker>();
            thisRigidbody = GetComponent<Rigidbody2D>();
            levelController = FindObjectOfType<LevelController>();
            stats = GetComponent<AIStats>();
        }

        private void Start()
        {
            maxForce = stats.GetMoveForce();
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }

        private void UpdatePath()
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(thisRigidbody.position, moveTarget, OnPathComplete);
            }
        }

        private void OnPathComplete(Path newPath)
        {
            if (!newPath.error)
            {
                path = newPath;
                currentWaypoint = 0;
            }
        }

        private void FixedUpdate()
        {
            if (moveTarget != null)
            {
                Move();
            }
        }

        private void Move()
        {
            if (path == null) { return; }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }

            ApplySteeringBehaviours();

            IncrementPathWaypoint();
        }

        private void ApplySteeringBehaviours()
        {
            Vector2 seek = SeekSteer();

            thisRigidbody.AddForce(seek);
        }

        private Vector2 SeekSteer()
        {
            Vector2 desiredVelocity = (Vector2)path.vectorPath[currentWaypoint] - thisRigidbody.position;
            float distance = desiredVelocity.magnitude;
            Vector2 desiredDirection = desiredVelocity.normalized;

            if (distance < slowingRadius)
            {
                float mappedSpeed = distance * moveSpeed / slowingRadius;
                desiredVelocity = desiredDirection * mappedSpeed;
            }
            else
            {
                desiredVelocity = desiredDirection * moveSpeed;
            }

            Vector2 steer = desiredVelocity - thisRigidbody.velocity;

            float currentMaxForce = maxForce;

            steer = Vector2.ClampMagnitude(steer, currentMaxForce);
            return steer;
        }

        private void IncrementPathWaypoint()
        {
            float distance = Vector2.Distance(thisRigidbody.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, nextWaypointDistance);
        }

        public void SetMoveTarget(Vector2 target)
        {
            moveTarget = target;
        }

        public void SetMaxForce(float amount)
        {
            maxForce *= amount;
        }

        public void ResetMoveSpeed()
        {
            maxForce = stats.GetMoveForce();
        }

    }
}