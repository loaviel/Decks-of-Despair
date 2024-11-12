using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab for the projectile
  

    void Update()
    {
        // Shoot in specified direction when arrow keys are pressed
        if (Input.GetKeyDown(KeyCode.UpArrow)) Shoot(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Shoot(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) Shoot(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Shoot(Vector2.right);
    }

    private void Shoot(Vector2 direction)
    {
        // Create a new projectile at the player's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Apply velocity to projectile based on direction and speed
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Retrieve speed from the projectile's own ProjectileStats component
            PlayerProjectileStats playerProjectileStats = projectile.GetComponent<PlayerProjectileStats>();
            if (playerProjectileStats != null)
            {
                rb.velocity = direction * playerProjectileStats.speed;
            }
        }
    }
}
