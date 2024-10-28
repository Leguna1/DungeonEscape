using UnityEngine;

public class AI_Behavior : MonoBehaviour
{
    private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private AI_Sight aiSight;

    public float moveSpeed = 1.5f;
    public float attackRange = 2.0f;
    public string acquireTarget;
    
    public float currentCooldown = 0.5f; // Start with no cooldown
    public float attackCooldownDuration;

    private Transform targetTransform;
    public Vector3[] patrolPoints;
    private int currentIndex = 0;
    private bool movingForward = true;
    
    public float attackDuration = 1.0f; // Duration for the attack animation to finish
    private float attackTimer = 0f; // Tracks attack animation time
    private bool isAttacking = false; // Tracks if AI is currently in an attack

    private void Start()
    {
        animator = GetComponent<Animator>();
        aiSight = GetComponent<AI_Sight>();
    }

    private void Update()
    {
        BehaviorManagement();
    }

    private void BehaviorManagement()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f) // Attack duration has finished
            {
                isAttacking = false;
                currentCooldown = attackCooldownDuration; // Reset cooldown after attack
                animator.SetBool(IsAttacking, false); // Stop attacking animation
            }
            return; // Exit if currently attacking to avoid other actions
        }
        if (aiSight.objectID == acquireTarget)
        {
            targetTransform = aiSight.transform; 
            float distanceToTarget = Vector3.Distance(transform.position, aiSight.objectLocation);
            
            // Check if within attack range
            if (distanceToTarget <= attackRange)
            {
                // Update cooldown timer
                if (currentCooldown > 0)
                {
                    currentCooldown -= Time.deltaTime;
                    Idle(); // Stop all animations if within range but cooldown active
                }
                else
                {
                    Attack(); // Attack if within range and cooldown is zero
                }
            }
            else
            {
                MoveToTarget(); // Move towards target if out of range
            }
        }
        else
        {
            StartPatrolling(); // Patrol if no target detected
        }
    }

    private void Idle()
    {
        animator.SetBool(IsWalking, false);
        animator.SetBool(IsAttacking, false);
    }

    private void MoveToTarget()
    {
        if (targetTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, aiSight.objectLocation, moveSpeed * Time.deltaTime);
            animator.SetBool(IsWalking, true); // Set walking animation while moving to the target
            animator.SetBool(IsAttacking, false);
        }
        else
        {
            StartPatrolling(); // Fall back to patrolling if no target is available
        }
    }

    private void Attack()
    {
        if (!isAttacking) // Start attack only if not already attacking
        {
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsAttacking, true);
            isAttacking = true;
            attackTimer = attackDuration; // Start the attack timer
        }
    }
    
    private void StartPatrolling()
    {
        if (patrolPoints.Length == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentIndex], moveSpeed * Time.deltaTime);
        animator.SetBool(IsWalking, true); // Walking animation while patrolling
        animator.SetBool(IsAttacking, false);

        // Check if AI has reached the patrol point
        if (Vector3.Distance(transform.position, patrolPoints[currentIndex]) < 0.1f)
        {
            // Determine next patrol point or reverse direction
            if (movingForward)
            {
                if (currentIndex < patrolPoints.Length - 1)
                {
                    currentIndex++;
                }
                else
                {
                    movingForward = false;
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
                    movingForward = true;
                    currentIndex++;
                }
            }
        }
    }
}
