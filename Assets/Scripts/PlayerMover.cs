using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;

    float startMoveSpeed;

    private void Awake()
    {
        startMoveSpeed = moveSpeed;
    }

    public void Move(Vector2 moveThrottle)
    {
        float currentMoveSpeed = moveSpeed;

        float moveSpeedX = moveThrottle.x * Time.fixedDeltaTime * currentMoveSpeed;
        float moveSpeedY = moveThrottle.y * Time.fixedDeltaTime * currentMoveSpeed;
        Vector2 moveForce = new Vector2(moveSpeedX, moveSpeedY);

        GetComponent<Rigidbody2D>().velocity = moveForce;
    }

    public void SetMoveSpeed(float amount)
    {
        moveSpeed = amount;
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = startMoveSpeed;
    }
}