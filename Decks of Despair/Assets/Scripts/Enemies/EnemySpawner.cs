using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab for the enemies
    public Transform[] spawnPoints; // Spawn points in the room
    public int enemiesPerWave = 5; // Number of enemies per wave
    public float timeBetweenWaves = 5f; // Time between waves
    public float spawnDelay = 0.5f; // Delay between each enemy spawn in a wave
    public float minSpawnDistance = 5f; // Minimum distance from the player for spawning enemies
    public Transform player; // Reference to the player 

    private bool spawning = false;
    private List<Transform> availableSpawnPoints = new List<Transform>(); // List to track available spawn points

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
            // Reset available spawn points for each wave
            availableSpawnPoints.Clear();
            availableSpawnPoints.AddRange(spawnPoints); // Add all spawn points initially

            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (availableSpawnPoints.Count > 0)
                {
                    SpawnEnemy();
                }
                else
                {
                    Debug.LogWarning("No available spawn points left.");
                    break;
                }

                // Wait before spawning the next enemy in the wave
                yield return new WaitForSeconds(spawnDelay);
            }

            // Wait before starting the next wave
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point, far enough from the player
        Transform spawnPoint = GetValidSpawnPoint();

        // Instantiate the enemy at the spawn point
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    private Transform GetValidSpawnPoint()
    {
        Transform spawnPoint = null;

        // Try to find a valid spawn point from the list of available points
        for (int i = 0; i < availableSpawnPoints.Count; i++)
        {
            Transform potentialSpawnPoint = availableSpawnPoints[i];

            // Check if the spawn point is at least 'minSpawnDistance' away from the player
            if (Vector3.Distance(potentialSpawnPoint.position, player.position) >= minSpawnDistance)
            {
                spawnPoint = potentialSpawnPoint;
                availableSpawnPoints.RemoveAt(i); // Remove this spawn point to avoid reusing it
                break; // Exit loop once a valid spawn point is found
            }
        }

        // If no valid spawn point is found, choose a random one from the remaining available points
        if (spawnPoint == null && availableSpawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            spawnPoint = availableSpawnPoints[randomIndex];
            availableSpawnPoints.RemoveAt(randomIndex); // Remove the chosen spawn point
        }

        return spawnPoint;
    }
}
