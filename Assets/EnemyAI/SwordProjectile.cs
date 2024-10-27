using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    private Vector3 moveDirection;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy projectile after a set lifetime
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

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction;
    }
}