using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 3;               // Player's max health
    public int currentHealth;               // Player's current health
    public float moveSpeed = 2f;            // Player's movement speed
    public SpriteRenderer spriteRenderer;   // Reference to the SpriteRenderer component
    public HealthUI healthUI;               // Reference to the HealthUI script
    public float flashDuration = 0.2f;      // Duration for the red flash effect
    public float damageCooldownTime = 0.5f;   // Time in seconds before player can take damage again (cooldown)

    private Color originalColor;            // Stores the original color
    private float lastDamageTime = -1f;     // Time when the player last took damage

    void Start()
    {
        // Initialize current health to max health at start
        currentHealth = maxHealth;

        healthUI.UpdateHealthUI();          // Initialize health UI

        // Get the SpriteRenderer component and save the original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void ApplyCardEffect(int healthChange, float speedChange)
    {
        // Modify health within bounds and apply speed change
        currentHealth = Mathf.Clamp(currentHealth + healthChange, 0, maxHealth);
        moveSpeed += speedChange;
    }

   

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                // Apply damage only if enough time has passed since the last damage
                if (Time.time >= lastDamageTime + damageCooldownTime)
                {
                    // Apply damage and update the last damage time
                    ApplyDamage(enemyStats.damage);
                    lastDamageTime = Time.time;  // Reset cooldown
                }
            }
        }
    }

    private void ApplyDamage(int damageAmount)
    {
        // Subtract damage from current health
        currentHealth -= damageAmount;

        // Flash red when the player is hit
        StartCoroutine(FlashRed());

        // Update health UI after taking damage
        healthUI.UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died!");
            Destroy(gameObject);
            // Add player death later
        }
    }

    private IEnumerator FlashRed()
    {
        // Change the color to red
        spriteRenderer.color = Color.red;

        // Wait for the specified flash duration
        yield return new WaitForSeconds(flashDuration);

        // Change the color back to the original color
        spriteRenderer.color = originalColor;
    }
}
