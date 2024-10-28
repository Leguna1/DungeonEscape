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
    
    private float currentCooldown = 0.5f;    // Tracks remaining time for cooldown

    private Transform targetTransform;
    public Vector3[] patrolPoints;
    private int currentIndex = 0;
    private bool movingForward = true;

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
        // Check if a target is detected
        if (aiSight.objectID == acquireTarget)
        {
            targetTransform = aiSight.transform; // Set the target if the acquireTarget is detected
            float distanceToTarget = Vector3.Distance(transform.position, aiSight.objectLocation);

            if (distanceToTarget <= attackRange)
            {
                // Update cooldown timer
                if (currentCooldown > 0)
                {
                    currentCooldown -= Time.deltaTime;
                }
                Attack();
            }
            else
            {
                MoveToTarget();
            }
        }
        else
        {
            // If no target, switch to patrolling
            StartPatrolling();
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
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, aiSight.objectLocation, moveSpeed * Time.deltaTime);
            animator.SetBool(IsWalking, true); // Play walking animation while moving to the target
            animator.SetBool(IsAttacking, false);
        }
        else
        {
            StartPatrolling(); // Fall back to patrolling if no target is available
        }
    }

    private void Attack()
    {
        // Only trigger the attack animation if within range
        animator.SetBool(IsWalking, false);
        animator.SetBool(IsAttacking, true);
    }
    
    private void StartPatrolling()
    {
        if (patrolPoints.Length == 0) return;

        // Move towards the current patrol point
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentIndex], moveSpeed * Time.deltaTime);
        animator.SetBool(IsWalking, true); // Play walking animation while patrolling
        animator.SetBool(IsAttacking, false);

        // Check if the AI has reached the current patrol point
        if (Vector3.Distance(transform.position, patrolPoints[currentIndex]) < 0.1f)
        {
            // Determine the next patrol point or direction
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
