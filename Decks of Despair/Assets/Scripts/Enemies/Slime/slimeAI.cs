using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SlimeAI : MonoBehaviour
{
    public Transform shadowPrefab;    // Prefab for the shadow indicator
    private Transform player;         // Reference to the player's transform
    private EnemyStats enemyStats;    // Reference to the slime's stats
    private Rigidbody2D rb;           // Rigidbody2D for movement
    private Collider2D slimeCollider; // Collider of the slime

    public float jumpCooldown = 2f;   // Cooldown time between jumps
    public float detectionRadius = 5f; // How close the slime needs to be to jump
    public float jumpForce = 5f;      // Force applied when jumping
    private float lastJumpTime;       // Tracks the last time the slime jumped
    private Transform shadowInstance; // Instance of the shadow indicator
    private bool isJumping = false;   // Flag to track if the slime is jumping

    public float airFollowSpeed = 1f; // How quickly the slime follows the shadow mid-air

    void Start()
    {
        // Locate the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the EnemyStats component to access moveSpeed and damage
        enemyStats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Continuously follow the player
        if (player != null && enemyStats != null && !isJumping)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * enemyStats.moveSpeed;

            // Call FacePlayer() to ensure the enemy faces the player
            FacePlayer();
        }

        // Check if the player is within detection radius and if the slime can jump
        if (player != null && Time.time >= lastJumpTime + jumpCooldown)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                StartCoroutine(JumpAndSlam());
            }
        }
    }


    private IEnumerator JumpAndSlam()
    {
        // Record the time of the jump
        lastJumpTime = Time.time;

        // Stop moving and start jumping
        rb.velocity = Vector2.zero;
        isJumping = true;

        // Disable collision with the player
        slimeCollider.enabled = false;

        // Calculate jump target (towards player)
        Vector2 jumpTarget = player.position;

        // Instantiate a shadow indicator at the target location
        shadowInstance = Instantiate(shadowPrefab, jumpTarget, Quaternion.identity);

        // Shrink the slime to simulate jumping
        Vector3 originalScale = transform.localScale;
        Vector3 shrunkenScale = originalScale * 0.5f;
        transform.localScale = shrunkenScale;

        // Follow shadow while in the air
        float airTime = 1f; // Total air time 
        float elapsedTime = 0f;

        while (elapsedTime < airTime)
        {
            // Move towards the shadow's position 
            Vector2 shadowDirection = ((Vector2)shadowInstance.position - (Vector2)transform.position).normalized;
            rb.velocity = shadowDirection * airFollowSpeed;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restore the slime's scale and teleport to the shadow position
        transform.localScale = originalScale;
        transform.position = shadowInstance.position;

        // Perform the slam at the shadow's position
        PerformSlam();

        // Re-enable collision with the player
        slimeCollider.enabled = true;

        // Destroy the shadow and end the jump
        if (shadowInstance != null)
        {
            Destroy(shadowInstance.gameObject);
        }
        isJumping = false;
    }

    private void PerformSlam()
    {
        // Detect all objects within the shadow's radius
        float shadowRadius = shadowPrefab.localScale.x / 2; 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(shadowInstance.position, shadowRadius);

        // Damage any player within the radius
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerStats playerStats = collider.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.ApplyDamage(enemyStats.damage);
                }
            }
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
