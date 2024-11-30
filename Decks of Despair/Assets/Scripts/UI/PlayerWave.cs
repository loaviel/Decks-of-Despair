using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerWave
{
    static private Image currentWave1;               // Image for the first digit of current wave
    static private Image currentWave2;               // Image for the second digit of current wave

    public static void SetPlayerScore(Image currentWave1Pass, Image currentWave2Pass)
    {
        // Sets the player score each time the wave changes, in order to update the game over screen on death.
        currentWave1 = currentWave1Pass;
        currentWave2 = currentWave2Pass;
    } 

    public static Image GetWaveTens()
    {
        // Allows for returning the wave tens
        return currentWave1;
    }

    public static Image GetWaveUnits()
    {
        // Allows for returning the wave units
        return currentWave2;
    }
} 
