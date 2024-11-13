using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerStats playerStats;          // Reference to the PlayerStats script
    public List<Image> healthImages;         // List of heart Images in the UI
    public Color fullHealthColor = Color.red;    // Color for full health hearts
    public Color emptyHealthColor = new Color(0.3f, 0.3f, 0.3f, 1f); // Color for empty hearts

    void Start()
    {
        // Initialize health UI to show full hearts
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < healthImages.Count; i++)
        {
            if (i < playerStats.currentHealth)
            {
                // Set to full health color if within the current health range
                healthImages[i].color = fullHealthColor;
            }
            else
            {
                // Set to empty health color if outside the current health range
                healthImages[i].color = emptyHealthColor;
            }
        }
    }
}
