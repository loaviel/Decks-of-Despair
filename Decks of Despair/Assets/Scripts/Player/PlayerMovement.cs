using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;           // Reference to player stats
    private Rigidbody2D rb;             // Reference to Rigidbody2D component
    private Vector2 movement;           // Stores movement direction

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;            // No gravity for 2D top-down movement
    }

    public void StopAllMovement()
    { 
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (CardSelectionManager.isInputLocked)
        {
            movement = Vector2.zero; // Prevent movement when input is locked
            return;
        }

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
        // Calculate velocity based on movement and stats.moveSpeed
        Vector2 desiredVelocity = movement * stats.moveSpeed;

        // Apply velocity directly for precise control
        rb.velocity = Vector2.Lerp(rb.velocity, desiredVelocity, 0.2f);
    }
}