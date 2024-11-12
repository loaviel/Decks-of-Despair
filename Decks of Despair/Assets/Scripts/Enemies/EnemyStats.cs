using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int health;   // Default health for all enemies -- to be changed
    public int damage;    // Default damage for all enemies -- to be changed
    public float moveSpeed; // Default speed for all enemies -- to be changed


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
