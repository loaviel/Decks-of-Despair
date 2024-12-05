using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.EventSystems;

public class SquareEnemyStats : EnemyStats
{

    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstantiated;



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

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        SpawnDamageParticles();
    }

    protected override void Die()
    {
        // Play the relevant death noise
        audioManager.WolfPlay();

        base.Die();

    }

    private void SpawnDamageParticles()
    {
        damageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
    }
}