using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] ParticleSystem AttackParticle;
    void Update()
    {
        
    }
    public void PlayDeathParticle(){
        DeathParticle.Play();
    }
    public void PlayHitParticle(){
        HitParticle.Play();
    }
    public void PlayAttackParticle(){
        AttackParticle.Play();
    }
}
