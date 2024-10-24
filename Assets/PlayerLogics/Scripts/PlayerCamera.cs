using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;       // The target the camera follows (usually the player)
    [SerializeField] private Vector3 offset;         // Offset from the target for the camera's position
    [SerializeField] private float rotationSpeed = 5f; // Speed at which the camera rotates
    [SerializeField] private float maxXAngle = 80f;  // Max angle the camera can tilt up
    [SerializeField] private float minXAngle = -80f; // Max angle the camera can tilt down

    private float yaw = 0f;   // Rotation around Y axis
    private float pitch = 0f; // Rotation around X axis

    // Input action for VR camera look
    public InputActionProperty lookVRInput;

    private bool isVRActive = false; // Check if VR is active

    private void Start()
    {
        CheckIfVRIsActive();
    }

    private void Update()
    {
        // Always follow the target's position with an offset
        FollowTarget();

        // Rotate the camera based on input (either VR or keyboard/mouse)
        if (isVRActive)
        {
            HandleVRCameraRotation();
        }
        else
        {
            HandleMouseCameraRotation();
        }
    }

    void CheckIfVRIsActive()
    {
        // Check if a VR device is currently active
        isVRActive = XRSettings.isDeviceActive;
        Debug.Log(isVRActive);
    }

    private void FollowTarget()
    {
        // Smoothly follow the target with the given offset
        transform.position = target.position + offset;
    }

    private void HandleMouseCameraRotation()
    {
        // Check if the right mouse button is being held down
        if (Input.GetMouseButton(1))
        {
            // Rotate camera based on mouse input
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            yaw += mouseX;
            pitch -= mouseY;  // Invert pitch for more intuitive controls

            // Clamp the pitch between the min and max angles
            pitch = Mathf.Clamp(pitch, minXAngle, maxXAngle);

            // Apply the rotations to the camera's transform
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }

    private void HandleVRCameraRotation()
    {
        // Get input from the right-hand joystick for VR camera rotation
        Vector2 lookInput = lookVRInput.action.ReadValue<Vector2>();

        // Rotate camera based on joystick input
        float joystickX = lookInput.x * rotationSpeed;
        float joystickY = lookInput.y * rotationSpeed;

        yaw += joystickX;
        pitch -= joystickY;  // Invert pitch for more intuitive controls

        // Clamp the pitch between the min and max angles
        pitch = Mathf.Clamp(pitch, minXAngle, maxXAngle);

        // Apply the rotations to the camera's transform
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
