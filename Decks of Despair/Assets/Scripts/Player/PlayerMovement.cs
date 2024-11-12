using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to player stats
    public PlayerStats stats;
    private Vector2 movement; // Stores movement direction

    void Start()
    {
        // Get PlayerStats component attached to the player
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Reset movement direction
        movement = Vector2.zero;

        // Check for movement input and set direction
        if (Input.GetKey(KeyCode.W))
            movement.y = 1;
        if (Input.GetKey(KeyCode.S))
            movement.y = -1;
        if (Input.GetKey(KeyCode.D))
            movement.x = 1;
        if (Input.GetKey(KeyCode.A))
            movement.x = -1;

        // Normalize direction to keep consistent speed
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Move player based on speed from stats and direction
        transform.Translate(stats.moveSpeed * Time.deltaTime * movement);
    }
}
