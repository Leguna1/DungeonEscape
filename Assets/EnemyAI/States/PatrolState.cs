using UnityEngine;

public class PatrolState : State
{
    public Vector3[] positions;       // Array of positions for NPC to move through
    public float speed = 1f;          // Movement speed of the NPC
    public float detectionRange = 10f; // Distance within which NPC can detect the player
    public LayerMask playerLayer;      // Layer mask to detect the player
    public Transform playerTransform;  // Reference to the player’s transform

    private int currentIndex = 0;      // Current target index in the positions array
    private bool movingForward = true; // Direction flag to track forward or backward movement
    private Transform grandparentTransform;

    public ChaseState chaseState;
    private bool canSeeThePlayer;
    
    private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Start()
    {
        // Get reference to the NPC's main transform
        grandparentTransform = transform.parent.parent;
        animator = transform.parent.parent.GetComponent<Animator>(); //Access grandparent animator component
    }

    public override State RunCurrentState()
    {
        DetectPlayer();
        
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            Patrol();
            return this;
        }
    }

    private void Patrol()
    {
        if (positions.Length == 0) return; // Exit if there are no positions set
        
        // Move the NPC's main object towards the target position
        grandparentTransform.position = Vector3.MoveTowards(grandparentTransform.position, positions[currentIndex], speed * Time.deltaTime);
        animator.SetBool(IsWalking, true);
        // Check if NPC has reached the target position
        if (Vector3.Distance(grandparentTransform.position, positions[currentIndex]) < 0.1f)
        {
            // Move to the next index or reverse if at an endpoint
            if (movingForward)
            {
                if (currentIndex < positions.Length - 1)
                {
                    currentIndex++;
                }
                else
                {
                    movingForward = false; // Start moving backward
                    currentIndex--;
                }
            }
            else
            {
                if (currentIndex > 0)
                {
                    currentIndex--;
                }
                else
                {
                    movingForward = true; // Start moving forward
                    currentIndex++;
                }
            }
        }
    }

    private void DetectPlayer()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = (playerTransform.position - grandparentTransform.position).normalized;
        float distanceToPlayer = Vector3.Distance(grandparentTransform.position, playerTransform.position);

        // Cast a ray from the NPC to the player within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(grandparentTransform.position, directionToPlayer, out hit, detectionRange, playerLayer))
            {
                if (hit.transform == playerTransform)
                {
                    canSeeThePlayer = true;
                    return;
                }
            }
        }
        
        // If the ray does not hit the player, set canSeeThePlayer to false
        canSeeThePlayer = false;
    }
}
