using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Transform closedTransform; // Reference to the closed state transform
    public Transform openTransform;   // Reference to the open state transform
    public float transitionSpeed = 2.0f; // Speed of the transition
    public bool isOpen = false; // Toggle to open/close the chest

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isTransitioning = false;

    private void Start()
    {
        // Initialize the target to the closed state
        targetPosition = closedTransform.position;
        targetRotation = closedTransform.rotation;
    }

    private void Update()
    {
        if (isTransitioning)
        {
            // Smoothly interpolate position and rotation
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionSpeed * Time.deltaTime);

            // Stop transitioning when close enough to the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f && 
                Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isTransitioning = false;
            }
        }
    }

    public void ToggleChest()
    {
        // Toggle the target state
        isOpen = !isOpen;

        if (isOpen)
        {
            targetPosition = openTransform.position;
            targetRotation = openTransform.rotation;
        }
        else
        {
            targetPosition = closedTransform.position;
            targetRotation = closedTransform.rotation;
        }

        isTransitioning = true; // Start the transition
    }
}