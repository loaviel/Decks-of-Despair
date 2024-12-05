using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlimeStats : EnemyStats
{

    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstantiated;

    new void Start()
    {
        base.Start();
     // Unique stats for slime enemy -- overriden from base EnemyStats
     health = 4;
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
        audioManager.SlimePlay();

        base.Die();
    }

    private void SpawnDamageParticles()
    {
        damageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
    }
}
