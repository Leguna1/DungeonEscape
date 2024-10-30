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
    public Vector3 respawnLocation;
    public float respawnDelay = 2.0f; 
    
    private CharacterControllerDriver playerMovement; 

    private void Start()
    {
        CurrentHealth = maxHealth;
        healthSlider.value = maxHealth;
        
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
           
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            
            Invoke(nameof(RespawnPlayer), respawnDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RespawnPlayer()
    {
        
        CurrentHealth = maxHealth;
        healthSlider.value = maxHealth;

        
        transform.position = respawnLocation;

        
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        Debug.Log($"{gameObject.name} has respawned at {respawnLocation}.");
    }
}
