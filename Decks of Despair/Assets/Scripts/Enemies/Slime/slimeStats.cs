using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStats : EnemyStats
{
    new void Start()
    {
        base.Start();
     // Unique stats for slime enemy -- overriden from base EnemyStats
     health = 4;
     damage = 1;
     moveSpeed = 2f;
    }

    protected override void Die()
    {
        // Play the relevant death noise
        audioManager.SlimePlay();

        base.Die();
    }
}
