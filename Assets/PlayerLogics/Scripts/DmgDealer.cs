using UnityEngine;

public class DmgDealer : MonoBehaviour
{
    public int damageAmount = 10; // Damage amount to apply on collision

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other object has a Health component
        Health healthComponent = collision.gameObject.GetComponent<Health>();

        if (healthComponent != null && healthComponent.CurrentHealth > 0)
        {
            // If Health component exists and health is greater than 0, deal damage
            healthComponent.TakeDamage(damageAmount);
            Debug.Log("Dmg" + damageAmount);
        }
    }
}
