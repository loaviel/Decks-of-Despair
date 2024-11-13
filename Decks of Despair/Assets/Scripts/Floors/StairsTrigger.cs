using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public GameObject tutorial; // Reference to the tutorial text object
    public EnemySpawner enemySpawner; // Reference to the enemy spawner

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide tutorial 
            tutorial.SetActive(false);

            // Start spawning enemies
            enemySpawner.StartSpawning();

            // Hide the stairs -- will make it so it turns back on after every clear later -- use for lvl progression
            gameObject.SetActive(false);
        }
    }
}
