using System.Collections;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab for the projectile
    public float teleportCooldown = 2f; // Cooldown time between teleports
    public float shootCooldown = 0.5f;  // Cooldown after teleport before shooting
    public float detectionRadius = 5f;  // Detection radius for teleport and shoot behavior
    public float moveSpeed = 3f;        // Movement speed of the ghost
    public float stopDistance = 2f;    // Distance to stop moving toward the player

    private Transform player;          // Reference to the player's transform
    private bool canShoot = true;      // Tracks if the ghost can shoot
    private bool canTeleport = true;   // Tracks if the ghost can teleport
    private bool isTeleporting = false; // Tracks if the ghost is in teleport cooldown
    private CapsuleCollider2D collider; // Reference to collider component

    void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        collider = GetComponent<CapsuleCollider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null || collider.enabled == false) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Move towards the player if not too close
        if (distanceToPlayer > stopDistance && !isTeleporting)
        {
            MoveTowardsPlayer();
        }

        // Call FacePlayer() to make sure the enemy faces the player
        FacePlayer();

        // Handle teleport and shooting logic
        if (distanceToPlayer <= detectionRadius && canTeleport)
        {
            StartCoroutine(TeleportAndShoot());
        }
    }


    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private IEnumerator TeleportAndShoot()
    {
        canTeleport = false; // Prevent teleporting again during cooldown
        canShoot = false;    // Prevent shooting right after teleporting
        isTeleporting = true;

        // Try to teleport to a random position around the player, but make sure it's not inside a barrier
        Vector2 teleportPosition = player.position + (Vector3)Random.insideUnitCircle.normalized * 2f;

        // Check if the teleport position is blocked by a barrier
        if (IsTeleportPositionBlocked(teleportPosition))
        {
            // If blocked, find a new teleport position 
            int retries = 5;
            while (retries > 0 && IsTeleportPositionBlocked(teleportPosition))
            {
                teleportPosition = player.position + (Vector3)Random.insideUnitCircle.normalized * 2f;
                retries--;
            }
        }

        // Set the teleport position, assuming we have a valid one
        transform.position = teleportPosition;

        yield return new WaitForSeconds(shootCooldown); // Wait before allowing shooting
        canShoot = true;

        // Shoot projectiles if allowed
        if (canShoot)
        {
            ShootAtPlayer();
        }

        yield return new WaitForSeconds(teleportCooldown - shootCooldown); // Wait for the remaining teleport cooldown
        canTeleport = true;
        isTeleporting = false;
    }

    private bool IsTeleportPositionBlocked(Vector2 position)
    {
        // Ssmall radius around the teleport position to check for barriers
        float checkRadius = 0.5f;

        // Check for colliders in the area around the teleport position
        Collider2D collider = Physics2D.OverlapCircle(position, checkRadius);

        // If there is a collider and it has the "Barrier" tag, return true
        if (collider != null && collider.CompareTag("Barrier"))
        {
            return true;
        }

        return false;
    }

    private void ShootAtPlayer()
    {
        if (projectilePrefab == null) return;

        // Instantiate the projectile and direct it towards the player
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GhostProjectile projectileStats = projectile.GetComponent<GhostProjectile>();
        if (projectileStats != null && player != null)
        {
            projectileStats.Initialize(player.position); // Pass player's position for movement direction
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
