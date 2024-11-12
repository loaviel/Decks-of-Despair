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
        }
    }
}
