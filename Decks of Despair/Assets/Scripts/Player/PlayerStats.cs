using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 3;        // Player's max health
    public int currentHealth;        // Player's current health
    public float moveSpeed = 2f;     // Player's movement speed

    void Start()
    {
        // Initialize current health to max health at start
        currentHealth = maxHealth;
    }

    public void ApplyCardEffect(int healthChange, float speedChange)
    {
        // Modify health within bounds and apply speed change
        currentHealth = Mathf.Clamp(currentHealth + healthChange, 0, maxHealth);
        moveSpeed += speedChange;
    }
}
