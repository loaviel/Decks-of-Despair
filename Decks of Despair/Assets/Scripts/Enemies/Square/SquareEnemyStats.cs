using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemyStats : EnemyStats
{
    void Start()
    {
       
        // Unique stats for square enemy -- overriden from base EnemyStats
        health = 5;
        damage = 1;
        moveSpeed = 1f;
    }

    
    protected override void Die()
    {
        Debug.Log("Square Enemy died!");
        base.Die();
    }
}
