using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPrefab[] enemyPrefabs;     // Array to store enemies and their spawn chances
    public Transform[] spawnPoints;        // Spawn points in the room
    public int enemiesPerWave = 8;         // Number of enemies per wave
    public float spawnDelay = 0.5f;        // Delay between each enemy spawn in a wave
    public float minSpawnDistance = 5f;    // Minimum distance from the player for spawning enemies
    public int maxWave = 0;                // Initially set to 0, will be updated when waves are triggered
    public Transform player;               // Reference to the player

    public int currentWave = 0;            // Track the current wave number
    private bool spawning = false;
    private List<Transform> availableSpawnPoints = new List<Transform>(); // List to track available spawn points

    private int remainingEnemies = 0;      // Track the number of remaining enemies in the current wave

    // Reference to the CardSelectionManager
    public CardSelectionManager cardSelectionManager;

    // UI elements for card selection
    public GameObject cardSelectionPopup;   // The popup UI for card selection
    private bool cardSelected = false;      // Flag to track if a card is selected

    void Start()
    {
        // Start spawning process based on selected enemies
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!spawning && maxWave > 0) // Ensure maxWave is greater than 0
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

            // Reset remaining enemies count at the start of each wave
            remainingEnemies = enemiesPerWave;

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

            // Wait for all enemies to be killed before showing the card selection popup
            yield return new WaitUntil(() => remainingEnemies == 0);

            // Proceed with card selection once all enemies are killed
            ShowCardSelectionPopup();

            // Wait for the player to select a card
            yield return new WaitUntil(() => cardSelected);

            // Reset the flag after the card selection
            cardSelected = false;

            Debug.Log("Wave " + currentWave + " completed!");

            // Addd a small delay between waves
            yield return new WaitForSeconds(1f);
        }

        // End spawning if maxWave is reached
        if (currentWave >= maxWave)
        {
            Debug.Log("All waves completed!");
            spawning = false;
        }
    }

    private void ShowCardSelectionPopup()
    {
        // Show the popup UI for card selection
        cardSelectionPopup.SetActive(true);
        cardSelected = false; // Reset the selection flag

        cardSelectionManager.ShowCardSelection();
    }

    public void OnCardSelected()
    {
        // Mark that a card has been selected and close the poup
        cardSelected = true;
        cardSelectionPopup.SetActive(false); // Hide the card selection popup

        // Reset for the next wave
        remainingEnemies = enemiesPerWave;
       

        // Proceed to spawn the next wave
        StartSpawning();
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point, far enough from the player
        Transform spawnPoint = GetValidSpawnPoint();

        if (spawnPoint != null)
        {
            // Choose a random enemy based on spawn chances
            GameObject enemyToSpawn = GetRandomEnemy();

            if (enemyToSpawn != null)
            {
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
                EnemyStats enemyStats = spawnedEnemy.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    // Subscribe to the OnDeath event
                    enemyStats.OnDeath += HandleEnemyDeath;
                }
                else
                {
                    Debug.LogWarning("No EnemyStats component found on spawned enemy.");
                }

                
            }
            else
            {
                Debug.LogWarning("No enemy prefab available to spawn.");
            }
        }
        else
        {
            Debug.LogWarning("No valid spawn point found.");
        }
    }

    private void HandleEnemyDeath()
    {
        remainingEnemies--; // Decrease the remaining enemy count when an enemy dies

        Debug.Log("Enemy died, remaining enemies: " + remainingEnemies);

        // Check if all enemies are dead
        if (remainingEnemies == 0)
        {
            // All enemies are dead, show the card selection 
            if (!cardSelected)  // Prevent multiple triggers of card selection
            {
                ShowCardSelectionPopup();
            }
        }
    }
    private GameObject GetRandomEnemy()
    {
        // Randomly choose an enemy based on their spawn chances
        float totalChance = 0;
        foreach (var enemy in enemyPrefabs)
        {
            totalChance += enemy.spawnChance;
        }

        float randomValue = Random.Range(0, totalChance);
        float cumulativeChance = 0;

        foreach (var enemy in enemyPrefabs)
        {
            cumulativeChance += enemy.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return enemy.enemyPrefab; // Return the selected enemy prefab
            }
        }

        return null;
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

    public void OnEnemyKilled()
    {
        // Decrease the remaining enemy count when an enemy is killed
        remainingEnemies--;
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

// Enemy prefab with spawn chance
[System.Serializable]
public class EnemyPrefab
{
    public GameObject enemyPrefab; // The prefab for the enemy
    public float spawnChance;      // The chance this enemy has to spawn
}
