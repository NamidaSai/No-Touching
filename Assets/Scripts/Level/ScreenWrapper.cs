using UnityEngine;

namespace Level
{
    public class ScreenWrapper : MonoBehaviour
    {
        [SerializeField] private float buffer = 1f;
        [SerializeField] private float distanceZ = 10f;
        
        private Camera mainCamera;
        private float leftConstraint;
        private float rightConstraint;
        private float bottomConstraint;
        private float topConstraint;

        private void Awake()
        {
            mainCamera = Camera.main;
            if (mainCamera == null) { return; }
            
            leftConstraint = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).x;
            rightConstraint = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, distanceZ)).x;
            bottomConstraint = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).y;
            topConstraint = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, distanceZ)).y;
        }

        private void FixedUpdate()
        {
            if (mainCamera == null) { return; }

            Vector2 position = transform.position;
            float targetX = position.x;
            float targetY = position.y;
            
            if (position.x < leftConstraint - buffer)
            {
                targetX = rightConstraint + buffer;
            }
            if (position.x > rightConstraint + buffer)
            {
                targetX = leftConstraint - buffer;
            }
            if (position.y < bottomConstraint - buffer)
            {
                targetY = topConstraint + buffer;
            }
            if (position.y > topConstraint + buffer)
            {
                targetY = bottomConstraint - buffer;
            }

            transform.position = new Vector3(targetX, targetY, distanceZ);
        }
    }
}