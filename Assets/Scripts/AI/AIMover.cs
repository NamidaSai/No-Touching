using UnityEngine;

namespace AI
{
    public class AIMover : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;

        Vector2 moveTarget;

        float maxForce;

        Rigidbody2D thisRigidbody;

        private void Awake()
        {
            thisRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
            Vector2 force = direction * moveSpeed;
            thisRigidbody.AddForce(force);
        }

        public void SetMoveSpeed(float statsSpeed)
        {
            moveSpeed = statsSpeed;
        }
    }
}