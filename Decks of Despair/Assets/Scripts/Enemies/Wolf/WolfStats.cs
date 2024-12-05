using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemyStats : EnemyStats
{

   // public Animation animation;
    new void Start()
    {
      //  animation = GetComponent<Animation>();

        base.Start();
       
        // Unique stats for wolf enemy -- overriden from base EnemyStats
        health = 3;
        damage = 1;
        moveSpeed = 2f;
    }


    protected override void Die()
    {
        // Play the relevant death noise
        audioManager.WolfPlay();
      //  animation.Play();
        base.Die();
    }
}
