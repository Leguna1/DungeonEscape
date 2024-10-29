using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float CurrentHealth { get; private set; }

    public Slider healthSlider;
    private float lerpHealthbarSpeed = 5.0f;

    // Respawn settings
    public Vector3 respawnLocation; // Set this in the inspector or code
    public float respawnDelay = 2.0f; // Time before the player respawns

    // Reference to movement component
    private CharacterControllerDriver playerMovement; // Assume a PlayerMovement script handles movement

    private void Start()
    {
        CurrentHealth = maxHealth;
        healthSlider.value = maxHealth;

        // Get the player movement component if this is the player
        if (CompareTag("Player"))
        {
            playerMovement = GetComponent<CharacterControllerDriver>();
        }
    }

    void Update()
    {
        if (healthSlider.value != CurrentHealth)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, CurrentHealth, lerpHealthbarSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        if (CompareTag("Player"))
        {
            // Disable player movement
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            // Start respawn process after delay
            Invoke(nameof(RespawnPlayer), respawnDelay);
        }
        else
        {
            // Destroy non-player objects
            Destroy(gameObject);
        }
    }

    private void RespawnPlayer()
    {
        // Reset health
        CurrentHealth = maxHealth;
        healthSlider.value = maxHealth;

        // Move the player to the respawn location
        transform.position = respawnLocation;

        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        Debug.Log($"{gameObject.name} has respawned at {respawnLocation}.");
    }
}
