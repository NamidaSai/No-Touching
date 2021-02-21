using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveThrottle;
    private Vector2 lookThrottle;
    Vector2 currentLookDirection;

    Coroutine firingCoroutine;

    bool firing = false;

    public void PlaySFX(string soundName)
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }

    private void FixedUpdate()
    {
        GetComponent<PlayerMover>().Move(moveThrottle);
        LookAt();
    }

    private void LookAt()
    {
        if (Gamepad.current == null)
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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

    public void OnFire()
    {
        if (FindObjectOfType<CanvasManager>().gamePaused) { return; }

        firing = !firing;

        if (firing)
        {
            firingCoroutine = StartCoroutine(GetComponent<Shooter>().Shoot());
        }
        else if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
        }
    }

    public void OnPause()
    {
        FindObjectOfType<CanvasManager>().GamePaused();
    }

}
