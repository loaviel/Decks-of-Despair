using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject waveTens;
    [SerializeField] private GameObject waveUnits;

    private void Start()
    {
        Image waveImageTens = waveTens.GetComponent<Image>();
        Image waveImageUnits = waveUnits.GetComponent<Image>();

        waveImageTens.sprite = PlayerWave.GetWaveTens().sprite;
        waveImageUnits.sprite = PlayerWave.GetWaveUnits().sprite;
    }
} 
