using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOldestEukaryote : MonoBehaviour
{
    public attackZone AttackZone;
    Animator animator;
    public float damage = 1f;
    public float KnockBackForce = 1f;

    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(AttackZone.DetectedObjects.Count > 0){
            animator.SetTrigger("Attack");
        } 
    }
    void OnCollisionEnter2D(Collision2D col){
            IDamageable damageable = col.collider.GetComponent<IDamageable>();
        if(damageable != null){
            
            Vector2 direction = (col.collider.transform.position - transform.position).normalized;
            Vector2 Knockback = direction * KnockBackForce;
            damageable.OnHit(damage, Knockback);
            }
            animator.SetTrigger("Attack");
            
        }
}
