using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveSpeed = 10.0f;
    private float horizontalMovement;

    [Header("Jump")]
    public float jumpPower = 5f;
    int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    private void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        isGrounded();
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0) {
            if (context.performed)
            {
                rb.linearVelocityY = jumpPower;
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                rb.linearVelocityY = rb.linearVelocityY * 0.5f;
                jumpsRemaining--;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }
}
