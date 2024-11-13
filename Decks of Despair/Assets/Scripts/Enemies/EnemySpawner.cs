using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab for the enemies
    public Transform[] spawnPoints; // Spawn points in the room
    public int enemiesPerWave = 5; // Number of enemies per wave
    public float timeBetweenWaves = 5f; // Time between waves
    public float spawnDelay = 0.5f; // Delay between each enemy spawn in a wave
   

    private bool spawning = false;

    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnWaves());
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (spawning)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();

                // Wait before spawning the next enemy in the wave
                yield return new WaitForSeconds(spawnDelay);
            }

            // Wait before starting the next wave
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the enemy at the spawn point
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
