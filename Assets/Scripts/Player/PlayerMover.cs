using UnityEngine;
using Audio;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float maxMoveAcceleration = 1f;
        [SerializeField] private ParticleSystem propelFX;
        [SerializeField] private SpriteRenderer propelSprite;

        private Rigidbody2D thisRigidbody;
        private Animator animator;
        private AudioManager audioManager;
        private bool isDisabled = false;

        private void Awake()
        {
            thisRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        public void Move(Vector2 moveThrottle)
        {
            if (isDisabled) { return; }
            
            if (moveThrottle.x != 0f)
            {
                Rotate(moveThrottle.x);
            }

            if (moveThrottle.y != 0f)
            {
                Propel(moveThrottle.y);
            }
            else
            {
                StopPropelFX();
            }
        }

        private void Rotate(float moveThrottleX)
        {
            Transform thisTransform = transform;
            Vector2 position = thisTransform.position;
            Vector2 turnThrottle = (Vector2)thisTransform.right * moveThrottleX;
            Vector2 targetDirection = position + turnThrottle;
            Vector2 desiredDirection = (targetDirection - position).normalized;
            float angle = Mathf.Atan2(desiredDirection.y, desiredDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion lookRotation = Quaternion.AngleAxis(angle, thisTransform.forward);
            thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, lookRotation, Time.deltaTime * rotationSpeed); 
        }

        private void Propel(float moveThrottleY)
        {
            Vector2 desiredDirection = transform.up;
            Vector2 desiredVelocity = desiredDirection * moveSpeed;
        
            Vector2 steer = desiredVelocity - thisRigidbody.velocity;
            float maxMoveForce = thisRigidbody.mass * maxMoveAcceleration;
            steer = Vector2.ClampMagnitude(steer, maxMoveForce);

            thisRigidbody.AddForce(steer);

            propelFX.Play();
            propelSprite.enabled = false;
            audioManager.Play("propel");
        }

        public void StopPropelFX()
        {
            propelFX.Stop();
            propelSprite.enabled = true;
            audioManager.Stop("propel");           
        }

        public void Disable()
        {
            StopPropelFX();
            isDisabled = true;
        }
    }
}