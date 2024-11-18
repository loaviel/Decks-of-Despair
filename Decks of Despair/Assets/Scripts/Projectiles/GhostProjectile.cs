using UnityEngine;

public class GhostProjectile : MonoBehaviour
{
    public int damage = 2;          // Damage dealt by the projectile
    public float speed = 15f;       // Speed of the projectile
    public float range = 7f;        // Maximum range the projectile can travel
    private Vector2 startPosition;  // Starting position of the projectile
    private Vector2 moveDirection;  // Direction to move the projectile

    public void Initialize(Vector2 targetPosition)
    {
        // Record the initial position and calculate direction towards the target
        startPosition = transform.position;
        moveDirection = (targetPosition - startPosition).normalized;
    }

    void Update()
    {
        // Move the projectile in the calculated direction
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Destroy the projectile if it exceeds its range
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Damage the player if hit
        if (other.CompareTag("Player"))
        {
            var playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.ApplyDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile after hitting the player
        }

        // Destroy the projectile if it hits a "Barrier"
        else if (other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
