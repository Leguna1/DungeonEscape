using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0); // Clamp health to a minimum of 0

        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        // Add death logic, disable movements and RespawnPlayer();
    }
}