using UnityEngine;

public class BaseMovement : MonoBehaviour
{
   
    public float rotationSpeed = 10f;  // Speed at which NPC rotates to face the movement direction

    private Vector3 previousPosition;  // Stores the previous position to calculate movement direction
    
    private void Start()
    {
        previousPosition = transform.position; // Initialize previous position
       
    }

    private void Update()
    {
       
        RotateBasedOnMovement();
       
    }
    
    // Calculates the rotation based on movement direction
    private void RotateBasedOnMovement()
    {
        // Calculate the direction vector from the previous position to the current position
        Vector3 movementDirection = transform.position - previousPosition;
        
        // Only rotate if there has been some movement
        if (movementDirection.magnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Update previousPosition for the next frame
        previousPosition = transform.position;
    }
}