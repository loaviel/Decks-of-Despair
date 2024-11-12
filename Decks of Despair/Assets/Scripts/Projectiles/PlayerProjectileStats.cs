using UnityEngine;

public class PlayerProjectileStats : MonoBehaviour
{
    public int damage = 1;          // Base damage of the projectile
    public float speed = 10f;        // Speed at which the projectile moves
    public float range = 5f;         // Maximum range projectile can travel

    private Vector2 startPosition;   // Starting position of the projectile

    void Start()
    {
        // Record the initial position for range checking
        startPosition = transform.position;
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
        if (other.CompareTag("Enemy"))
        {
            // Get the EnemyStats component on the enemy
            var enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage); // Apply damage to the enemy
            }
            Destroy(gameObject); // Destroy the projectile after it hits the enemy
        }
    }
}
