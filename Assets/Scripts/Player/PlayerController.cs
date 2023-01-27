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

        private PlayerMover mover;
        private Camera mainCamera;
        private CanvasManager canvasManager;
        private Shooter shooter;

        private void Awake()
        {
            mainCamera = Camera.main;
            canvasManager = FindObjectOfType<CanvasManager>();
            mover = GetComponent<PlayerMover>();
            shooter = GetComponent<Shooter>();
        }

        private void FixedUpdate()
        {
            mover.Move(moveThrottle);
        }

        private void OnMove(InputValue value)
        {
            moveThrottle = value.Get<Vector2>();
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