using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAvatar : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Rigidbody2D rb;

    private float moveInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private Animator anim;

    private bool isGrounded;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Jump.performed += OnJump;

        inputActions.Player.Fire.performed += OnFire;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Jump.performed -= OnJump;

        inputActions.Player.Fire.performed -= OnFire;

        inputActions.Disable();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (rb.linearVelocity.x == 0)
        {
            anim.SetInteger("Velocity", 0);
        }
        else
        {
            anim.SetInteger("Velocity", (int)rb.linearVelocity.x);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        anim.SetTrigger("Attack");
    }
}