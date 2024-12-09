using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStats : MonoBehaviour
{
    public int health;    // Default health for all enemies
    public int damage;    // Default damage for all enemies
    public float moveSpeed; // Default speed for all enemies

    public float flashDuration = 0.2f;      // Duration for the red flash effect
    private Color originalColor;

    private Transform player; // Reference to the player's transform
    private SpriteRenderer spriteRenderer;

    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler OnDeath;  // Event to be triggered on death

    public GameAudioManager audioManager;
    private Animator anim; 

    protected virtual void Start()
    {

        // Locate the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Locate GameAudioManager using "Audio" tag
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<GameAudioManager>();  

        // Get the SpriteRenderer component
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        anim = GetComponent<Animator>();

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
    public virtual void TakeDamage(int amount)
    {
        if (health != 1)
        {
            // Flash red when the enemy is hit only if it's not the last hit point
            StartCoroutine(FlashRed());
        }

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    // Method to die
    protected virtual void Die()
    {
        // Code that handles death animation
        if (anim != null)
        {
            anim.SetBool("Dying", true);

            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;


            Invoke("DestroyObject", 45f);
        }
        else
        {
            Destroy(gameObject); // Destroys the enemy GameObject
        }

        OnDeath?.Invoke();

        
        
    }

    private void DestoryObject()
    {
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
