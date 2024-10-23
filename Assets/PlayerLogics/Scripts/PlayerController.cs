using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 2.0f;

    // Animator to control player animations
    private Animator animator;

    // Movement animations
    private readonly string moveForwardAnimation = "MoveForward";
    private readonly string moveBackwardAnimation = "MoveBackward";
    private readonly string leftStrafeAnimation = "LeftStrafe";
    private readonly string rightStrafeAnimation = "RightStrafe";
    private readonly string idleAnimation = "Idle";

    private CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;

    private bool isVRActive = false; // To detect VR or keyboard input

    void Start()
    {
        // Assign the character controller and animator
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Check if VR is active at the start
        CheckIfVRIsActive();
    }

    void Update()
    {
        if (isVRActive)
        {
            // Handle VR Movement
            HandleVRMovement();
        }
        else
        {
            // Handle Keyboard/Mouse Movement
            HandleKeyboardMovement();
        }
    }

    private void CheckIfVRIsActive()
    {
        isVRActive = XRSettings.isDeviceActive;
    }

    private void HandleKeyboardMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys (strafe left and right)
        float verticalInput = Input.GetAxis("Vertical");     // W and S keys (move forward and backward)

        // Camera-aligned movement direction
        moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        // Move the player character
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        // Rotate player with the camera (on Y-axis only)
        RotateWithCamera();

        // Play animations based on movement
        PlayMovementAnimations(horizontalInput, verticalInput);
    }

    private void RotateWithCamera()
    {
        // Get the camera's rotation (only the Y-axis)
        float cameraYRotation = Camera.main.transform.eulerAngles.y;

        // Rotate the player character to face the same direction as the camera (Y-axis rotation)
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraYRotation, 0), rotationSpeed * Time.deltaTime);
    }

    private void PlayMovementAnimations(float horizontalInput, float verticalInput)
    {
        if (verticalInput > 0)
        {
            // Moving forward
            animator.Play(moveForwardAnimation);
        }
        else if (verticalInput < 0)
        {
            // Moving backward
            animator.Play(moveBackwardAnimation);
        }
        else if (horizontalInput < 0)
        {
            // Moving left (A key)
            animator.Play(leftStrafeAnimation);
        }
        else if (horizontalInput > 0)
        {
            // Moving right (D key)
            animator.Play(rightStrafeAnimation);
        }
        else
        {
            // Idle if no input
            animator.Play(idleAnimation);
        }
    }

    private void HandleVRMovement()
    {
        // Handle VR movement here (using XR input system)
        // Similar to HandleKeyboardMovement but with VR inputs.
    }
}
