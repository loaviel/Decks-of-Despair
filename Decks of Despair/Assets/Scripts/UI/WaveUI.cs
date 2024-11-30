using UnityEngine;
using UnityEngine.UI;


public class WaveUI : MonoBehaviour
{
    public EnemySpawner waveSpawner;                    // Reference to the EnemySpawner script
    public Image currentWave1;                          // Image for the first digit of current wave
    public Image currentWave2;                          // Image for the second digit of current wave
    public Image maxWave1;                              // Image for the first digit of max wave
    public Image maxWave2;                              // Image for the second digit of max wave
    public Sprite[] numberSprites;                      // Array of sprites for numbers 0-9
    [SerializeField] private PlayerStats playerStats;   // Reference to player stats
 
    private void Update()
    {
        UpdateWaveDisplay();
    }

    private void UpdateWaveDisplay()
    {
        int currentWave = waveSpawner.GetCurrentWave();
        int maxWaveValue = waveSpawner.GetMaxWave();

        // Convert current wave to two digits
        int currentWaveTens = currentWave / 10;
        int currentWaveOnes = currentWave % 10;

        // Set the images for current wave
        currentWave1.sprite = numberSprites[currentWaveTens];
        currentWave2.sprite = numberSprites[currentWaveOnes];

        // Update the player wave holder

        PlayerWave.SetPlayerScore(currentWave1, currentWave2);

        // Only update max wave display if maxWaveValue is greater than 0
        if (maxWaveValue > 0)
        {
            // Convert max wave to two digits
            int maxWaveTens = maxWaveValue / 10;
            int maxWaveOnes = maxWaveValue % 10;

            // Set the images for max wave
            maxWave1.sprite = numberSprites[maxWaveTens];
            maxWave2.sprite = numberSprites[maxWaveOnes];
        }
        else
        {
            // Set max wave display to "00" if maxWave is 0
            maxWave1.sprite = numberSprites[0];
            maxWave2.sprite = numberSprites[0];

            // Reset projectile and player stat changes
            playerStats.ResetStats();
        }
    }

}
