using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // Prefab for the regular enemies
    public GameObject slimePrefab;         // Prefab for the slime enemy
    public Transform[] spawnPoints;        // Spawn points in the room
    public int enemiesPerWave = 5;         // Number of enemies per wave
    public float timeBetweenWaves = 5f;    // Time between waves
    public float spawnDelay = 0.5f;        // Delay between each enemy spawn in a wave
    public float minSpawnDistance = 5f;    // Minimum distance from the player for spawning enemies
    public int maxWave = 0;                // Initially set to 0, will be updated when stairs are triggered
    public Transform player;               // Reference to the player 

    public int currentWave = 0;            // Track the current wave number
    private bool spawning = false;
    private List<Transform> availableSpawnPoints = new List<Transform>(); // List to track available spawn points

    private int spawnCounter = 0;          // Tracks the total number of enemies spawned

    public void StartSpawning()
    {
        if (!spawning && maxWave > 0)      // Only start if maxWave is greater than 0
        {
            spawning = true;
            StartCoroutine(SpawnWaves());
        }
    }

    public void SetMaxWave(int newMaxWave)
    {
        maxWave = newMaxWave;
    }

    private IEnumerator SpawnWaves()
    {
        while (spawning && currentWave < maxWave)
        {
            // Increase the wave count and display it
            currentWave++;
            Debug.Log("Starting Wave: " + currentWave);

            // Reset available spawn points for each wave
            availableSpawnPoints.Clear();
            availableSpawnPoints.AddRange(spawnPoints);

            // Spawn enemies for the current wave
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

        // End spawning if maxWave is reached
        if (currentWave >= maxWave)
        {
            Debug.Log("All waves completed!");
            spawning = false; // Stop spawning
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point, far enough from the player
        Transform spawnPoint = GetValidSpawnPoint();

        if (spawnPoint != null)
        {
            // Increment the spawn counter
            spawnCounter++;

            // Decide whether to spawn a slime or a regular enemy
            GameObject enemyToSpawn = (spawnCounter % 5 == 0) ? slimePrefab : enemyPrefab;

            // Instantiate the selected enemy at the spawn point
            Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        }
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

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public int GetMaxWave()
    {
        return maxWave;
    }
}
