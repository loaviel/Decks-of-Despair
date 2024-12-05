using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStats : EnemyStats
{
    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstantiated;
    new void Start()
    {
        base.Start();
        // Unique stats for ghost enemy -- overridden from base EnemyStats
        health = 3;            
        damage = 1;             
        moveSpeed = 3f;         
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        SpawnDamageParticles();
    }

    protected override void Die()
    {
        // Play the relevant death noise
        audioManager.GhostPlay();                
         
        base.Die();
    }

    private void SpawnDamageParticles()
    {
        damageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
    }

}
