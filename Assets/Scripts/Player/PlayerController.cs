using Audio;
using Combat;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 moveThrottle;
        private Vector2 lookThrottle;
        Vector2 currentLookDirection;

        Coroutine firingCoroutine;

        private AudioManager audioManager;
        private PlayerMover mover;
        private Camera mainCamera;
        private CanvasManager canvasManager;
        private Shooter shooter;

        private void Awake()
        {
            mainCamera = Camera.main;
            audioManager = FindObjectOfType<AudioManager>();
            canvasManager = FindObjectOfType<CanvasManager>();
            mover = GetComponent<PlayerMover>();
            shooter = GetComponent<Shooter>();
        }

        public void PlaySFX(string soundName)
        {
            audioManager.Play(soundName);
        }

        private void FixedUpdate()
        {
            mover.Move(moveThrottle);
            LookAt();
        }

        private void LookAt()
        {
            if (Gamepad.current == null)
            {
                Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                currentLookDirection = (mouseWorldPosition - (Vector2)transform.position).normalized;
            }
            else if (lookThrottle.magnitude > 0.1f)
            {
                currentLookDirection = lookThrottle;
            }

            transform.up = currentLookDirection;
        }

        private void OnMove(InputValue value)
        {
            moveThrottle = value.Get<Vector2>();
        }

        private void OnLook(InputValue value)
        {
            lookThrottle = value.Get<Vector2>();
        }

        public void OnFire(InputValue value)
        {
            if (canvasManager.gamePaused) { return; }

            shooter.isShooting = value.isPressed;
        }

        public void OnPause()
        {
            canvasManager.GamePaused();
        }

    }
}