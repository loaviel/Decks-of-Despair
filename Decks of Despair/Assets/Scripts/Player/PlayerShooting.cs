using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab for the projectile
    private float nextFireTime = 0f;     // Tracks the next time the player can fire a projectile
    public float fireRate = 0.45f;       // Time between shots

    void Update()
    {

        if (CardSelectionManager.isInputLocked) return; // Prevent shooting when input is locked

            
        // Only shoot if the projectile can fire 
        if (Time.time >= nextFireTime)
        {
            // Shoot in the specified direction when arrow keys are pressed
            if (Input.GetKey(KeyCode.UpArrow)) Shoot(Vector2.up);
            else if (Input.GetKey(KeyCode.DownArrow)) Shoot(Vector2.down);
            else if (Input.GetKey(KeyCode.LeftArrow)) Shoot(Vector2.left);
            else if (Input.GetKey(KeyCode.RightArrow)) Shoot(Vector2.right);
        }
    }

    private void Shoot(Vector2 direction)
    {
        // Offset to avoid projectile spawning inside the player or walls
        Vector2 spawnOffset = direction * 0.5f; 

        // Create a new projectile at the player's position with the offset
        GameObject projectile = Instantiate(projectilePrefab, (Vector2)transform.position + spawnOffset, Quaternion.identity);

        // Get the PlayerProjectileStats component from the instantiated projectile
        PlayerProjectileStats stats = projectile.GetComponent<PlayerProjectileStats>();

        if (stats != null)
        {
            // Apply velocity using the projectile's stats
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * stats.speed;  // Set velocity based on the direction and speed
            }

            // Update the next fire time based on the fire rate
            nextFireTime = Time.time + fireRate;
        }
    }
}
