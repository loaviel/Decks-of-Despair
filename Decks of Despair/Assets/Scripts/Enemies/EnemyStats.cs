using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;    // Default health for all enemies
    public int damage;    // Default damage for all enemies
    public float moveSpeed; // Default speed for all enemies

    private Transform player; // Reference to the player's transform
    private SpriteRenderer spriteRenderer; // The sprite renderer to flip

    protected virtual void Start()
    {
        // Locate the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the SpriteRenderer to manipulate the flip property
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Method to handle taking damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to die
    protected virtual void Die()
    {
        Destroy(gameObject); // Destroys the enemy GameObject
    }


}
