using UnityEngine;

public class SquareEnemyAI : MonoBehaviour
{
    private Transform player;       // Reference to the player's transform
    private EnemyStats enemyStats;  // Reference to the enemy's stats

    void Start()
    {
        // Locate the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the EnemyStats component to access moveSpeed
        enemyStats = GetComponent<EnemyStats>();

        
    }

    void Update()
    {
        // Ensure the player exists before trying to move towards them
        if (player != null && enemyStats != null)
        {
            // Calculate the direction towards the player and normalize it
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player using moveSpeed from EnemyStats
            transform.position += (Vector3)direction * enemyStats.moveSpeed * Time.deltaTime;

            // Call FacePlayer() to ensure the enemy faces the player
            FacePlayer();
        }
    }

    // Method to make the enemy face the player
    private SpriteRenderer spriteRenderer; // The sprite renderer to flip
    public void FacePlayer()
    {
        if (player == null || spriteRenderer == null) return;

        // Check if the player is to the left or right of the enemy
        float directionToPlayer = player.position.x - transform.position.x;

        // Flip the sprite horizontally depending on player position
        if (directionToPlayer > 0)  // Player is to the right
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (directionToPlayer < 0) // Player is to the left
        {
            spriteRenderer.flipX = true; // Facing left
        }
    }

}
