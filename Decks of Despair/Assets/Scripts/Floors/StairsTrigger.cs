using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public GameObject tutorial; // Reference to the tutorial text object
    public EnemySpawner enemySpawner; // Reference to the enemy spawner
    public int maxWavesAfterTrigger = 10; // Set max waves after trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide tutorial 
            tutorial.SetActive(false);

            // Set max wave and start spawning enemies
            enemySpawner.SetMaxWave(maxWavesAfterTrigger);
            enemySpawner.StartSpawning();

            // Hide the stairs -- will make it so it turns back on after every clear later -- use for lvl progression // maybe
            gameObject.SetActive(false);
        }
    }
}
