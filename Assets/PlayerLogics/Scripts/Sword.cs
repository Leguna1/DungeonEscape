using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the sword
    public float cooldownTime = 1.0f; // Cooldown time in seconds
    private float lastAttackTime = 0f; // Last time the sword dealt damage

    private void OnTriggerEnter(Collider other)
    {
        // Check if the cooldown period has passed
        if (Time.time - lastAttackTime < cooldownTime)
        {
            Debug.Log("Attack is on cooldown!");
            return; // Exit if on cooldown
        }

        // Record the time of this attack
        lastAttackTime = Time.time;

        // Check if the collider belongs to a shield
        if (other.CompareTag("Shield"))
        {
            Debug.Log("Attack blocked by the shield!");
            // Don't deal damage if the shield is hit
            return; 
        }

        // Check if the collided object has a Health component
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            // Apply damage to the health component
            health.TakeDamage(damage);
            Debug.Log($"Dealt {damage} damage to {other.gameObject.name}.");
        }
    }
}