using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    private Vector3 moveDirection;
    public int damageAmount = 10; // Damage amount to apply on collision

    
    private void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime; // Move the projectile in the specified direction
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle damage logic here, such as calling a TakeDamage method on the enemy
            Debug.Log("Projectile hit: " + other.name);
        }

        Destroy(gameObject); // Destroy projectile on collision
    }
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
    public void Initialize(Vector3 direction)
    {
        moveDirection = direction;
    }
}