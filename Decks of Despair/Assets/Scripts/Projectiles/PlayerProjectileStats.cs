using UnityEngine;

public class PlayerProjectileStats : MonoBehaviour
{
    public int damage = 1;          // Base damage of the projectile
    public float speed = 10f;       // Speed at which the projectile moves
    public float range = 5f;        // Maximum range the projectile can travel
    public float fireRate = 0.2f;   // Time between shots in seconds 
    public float nextFireTime = 0f; // Tracks when the player can shoot next

    private Vector2 startPosition;  // Starting position of the projectile

    void Start()
    {
        // Record the initial position for range checking
        startPosition = transform.position;

        // Slightly adjust starting Y position for visual effect
        startPosition.y += 0.6f;

        // Update the projectile's position to the new start position
        transform.position = startPosition;
    }

    void Update()
    {
        // Destroy projectile if it exceeds its specified range
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collisions with "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            var enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);  // Apply damage to the enemy
            }
            Destroy(gameObject);  // Destroy the projectile after it hits the enemy
        }

        // Destroy the projectile if it hits a "Barrier"
        else if (other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
