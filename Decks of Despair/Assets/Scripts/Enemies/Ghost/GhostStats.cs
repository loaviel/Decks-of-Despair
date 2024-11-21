using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStats : EnemyStats
{
    new void Start()
    {
        base.Start();
        // Unique stats for ghost enemy -- overridden from base EnemyStats
        health = 3;            
        damage = 1;             
        moveSpeed = 3f;         
    }
}
