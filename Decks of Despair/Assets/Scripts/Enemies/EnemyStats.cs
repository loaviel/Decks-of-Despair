using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;    // Default health for all enemies
    public int damage;    // Default damage for all enemies
    public float moveSpeed; // Default speed for all enemies


    public float flashDuration = 0.2f;      // Duration for the red flash effect
    private Color originalColor;

    private Transform player; // Reference to the player's transform
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        // Locate the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;


        // Get the SpriteRenderer component
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Log if spriteRenderer is null or not
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not attached to " + gameObject.name, this);
        }
        else
        {
            Debug.Log("SpriteRenderer found for " + gameObject.name);
        }

        originalColor = spriteRenderer?.color ?? Color.white; // Default to white if null
    }






    // Method to handle taking damage
    public void TakeDamage(int amount)
    {
        // Flash red when the enemy is hit
        StartCoroutine(FlashRed());
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    // Method to die
    protected virtual void Die()
    {
        // Flash red when the enemy is hit
        StartCoroutine(FlashRed());
        Destroy(gameObject); // Destroys the enemy GameObject
    }


    private IEnumerator FlashRed()
    {
        // Ensure spriteRenderer is not null before trying to change its color
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is null. Cannot flash red!", this);
            yield break;  // Exit the coroutine if spriteRenderer is null
        }

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }


}
