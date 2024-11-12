using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public int damage = 10;          // Base damage of the projectile
    public float speed = 10f;        // Speed at which the projectile moves
    public float range = 5f;         // Maximum range projectile can travel

    private Vector2 startPosition;   // Starting position of the projectile

    void Start()
    {
        // Record the initial position for range checking
        startPosition = transform.position;
    }

    void Update()
    {
        // Destroy projectile if it exceeds its specified range
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }
}
