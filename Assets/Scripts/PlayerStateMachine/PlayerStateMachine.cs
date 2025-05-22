using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    public Animator animator;

    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveSpeed = 10.0f;
    private float horizontalMovement;
    bool _isMovePressed;

    [Header("Jump")]
    bool _isJumpPressed;
    bool isJumping;
    public float jumpPower = 5f;
    int maxJumps = 1;
    int _jumpsRemaining = 1;
    bool _requireNewJumpPress;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("WallCheck")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask wallLayer;

    [Header("Flip")]
    bool isFacingRight = true;

    [Header("WallSlide")]
    public float wallSlideMovement = 2f;
    bool isWallSliding = false;
    public float wallSlideSpeed = 2f;

    [Header("WallJumping")]
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);

    [Header("Dashing")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.1f;
    public bool isDashing = false;
    public bool canDash = false;
    TrailRenderer trailRenderer;

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //getter and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; }}
    public bool IsMovePressed { get { return _isMovePressed; }}
    
    public Animator Animator { get { return animator; }}
    public int JumpsRemaining { get { return _jumpsRemaining; } set { _jumpsRemaining = value; } }
    public float JumpPower { get { return jumpPower; }}
    public int MaxJumps { get { return maxJumps; }}
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public Rigidbody2D Rb { get { return rb; }}
    public bool IsGrounded { get { return isGrounded; }}


    private void Awake()
    {
        // setup staet
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateStates();
        GroundCheck();

        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        CheckFlip();
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        _isMovePressed = horizontalMovement < 0 || horizontalMovement > 0;
    }

    public void PlayerDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        trailRenderer.emitting = true;

        float dashDirection = isFacingRight ? 1f : -1f;

        //rb.linearVelocityX = dashDirection * dashSpeed;
        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0);

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        isDashing = false;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }
    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;
    }
    private void CheckFlip()
    {
        if ((isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            FlipPlayer();
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            JumpsRemaining = maxJumps;
            isGrounded = true;
            isWallJumping = false;
            isWallSliding = false;
        }
        else
        {
            isGrounded = false;
        }
    }
}
