using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameInput gameInput;
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [Range(5f, 15f)] public float lerpIntencity = 1f;
    [Range(0f, 2f)] public float crouchHeight = 1f;
    InputAction move;
    InputAction jump;
    InputAction crouch;
    Vector3 velocity;
    Vector3 moveInput;
    bool isCrouching = false;
    void Awake()
    {
        gameInput = new GameInput();
    }

    void OnEnable()
    {
        move = gameInput.Player.Move; 
        jump = gameInput.Player.Jump;
        crouch = gameInput.Player.Crouch;

        move.Enable();
        jump.Enable();
        crouch.Enable();

        move.performed += OnMoveInput;
        move.canceled += OnMoveInput;
        jump.performed += OnJumpInput;
        crouch.performed += OnCrouchInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = new Vector3(context.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        var isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.1f);
        rb.velocity = (isGrounded) ? new Vector3(rb.velocity.x, jumpForce, rb.velocity.z) : rb.velocity;
    }

    private void OnCrouchInput(InputAction.CallbackContext context)
    {
        var cameraPosition = Camera.main.transform.position;
        if (!isCrouching)
        {
            cameraPosition.y -= crouchHeight;
            isCrouching = true;
            moveSpeed -= 2;
        }
        else if (isCrouching)
        {
            cameraPosition.y += crouchHeight;
            isCrouching = false;
            moveSpeed += 2;
        }
        Camera.main.transform.position = cameraPosition;
    }

    void OnDisable()
    {
        move.Disable();
        jump.Disable();
        crouch.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }

    void HorizontalMovement()
    {
        var moveDirection = Vector3.zero;

        moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * moveInput;

        velocity.x = Mathf.Lerp(velocity.x, moveDirection.x * moveSpeed, lerpIntencity * Time.fixedDeltaTime);
        velocity.z = Mathf.Lerp(velocity.z, moveDirection.z * moveSpeed, lerpIntencity * Time.fixedDeltaTime);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
