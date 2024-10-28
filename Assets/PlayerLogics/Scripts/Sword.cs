using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 10; 
    public float cooldownTime = 1.0f; 
    private float lastAttackTime = 0f; 
    
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
        Health health = other.GetComponent<Health>();
        if (other.CompareTag("Player") && health != null)
        {
            health.TakeDamage(damage);
            Debug.Log($"Dealt {damage} damage to {other.gameObject.name}.");
        }
    }
}