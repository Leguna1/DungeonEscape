using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public Transform playerTransform; // Reference to the player's transform
    public float chaseSpeed = 3f;     // Speed at which NPC chases the player
    public float attackRange = 1.5f;  // Distance at which NPC can attack the player
    private Transform grandparentTransform;
    public bool isInAttackRange;

    private void Start()
    {
        // Get reference to the NPC's main transform (parent's parent)
        grandparentTransform = transform.parent.parent;
    }

    public override State RunCurrentState()
    {
        // Check if NPC is within attack range of the player
        if (Vector3.Distance(grandparentTransform.position, playerTransform.position) <= attackRange)
        {
            isInAttackRange = true;
            return attackState;
        }
        else
        {
            isInAttackRange = false;
            ChasePlayer();
            return this;
        }
    }

    private void ChasePlayer()
    {
        // Move the NPC's main object towards the player’s position
        Vector3 directionToPlayer = (playerTransform.position - grandparentTransform.position).normalized;
        grandparentTransform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
    }
}