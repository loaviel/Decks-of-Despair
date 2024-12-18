using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public PlayerShooting playerShooting;
    public PlayerProjectileStats projectileStats;

    void Start()
    {
        // Initialize current health to max health at start
        currentHealth = maxHealth;

        // Initialize health UI
        healthUI.UpdateHealthUI();   

        // Get the SpriteRenderer component and save the original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        // Call reset method
        ResetStats();
    }

    public void ResetStats()
    {
        // Reset values to defaults 
        moveSpeed = 10f; 
        projectileStats.range = 5f;
        projectileStats.speed = 10f;
    }

    public void ApplyCardEffect(float speedChange, float fireRateChange, float rangeChange, float shotSpeedChange, int healthChange)
    {
        // Modify stat changes to player
       
        moveSpeed += speedChange;

        // Modify stat changes tot projectiles

        projectileStats.range += rangeChange;
        projectileStats.speed += shotSpeedChange;

        // Modify stat changes to player shooting

        playerShooting.fireRate -= fireRateChange;

        // Modify health and ensure UI updates
        maxHealth += healthChange;
        currentHealth += healthChange;

        // Prevent overhealing
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Update health UI to reflect changes
        healthUI.UpdateHealthUI();
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
                    GetComponent<AudioSource>().Play();
                    // Apply damage and update the last damage time
                    ApplyDamage(enemyStats.damage);
                    lastDamageTime = Time.time;  // Reset cooldown
                }
            }
        }
    }

    public void ApplyDamage(int damageAmount)
    {
        // Subtract damage from current health
        currentHealth -= damageAmount;

        // Flash red when the player is hit
        StartCoroutine(FlashRed());

        // Update health UI after taking damage
        healthUI.UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Loads the game over sceen once the player is dead
            SceneManager.LoadScene("GameOver");

            // Destroys the game object
            Destroy(gameObject);
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
