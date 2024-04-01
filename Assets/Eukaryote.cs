using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eukaryote : MonoBehaviour
{
    // Start is called before the first frame update
    public attackZone AttackZone;
    Animator animator;
    public float damage = 5f;
    public float KnockBackForce = 1f;
    public DetectionZone detectionZone;
    bool canMove = true;
    public float moveSpeed = 50f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
         if(detectionZone.detectedObjects.Count > 0){
            animator.SetBool("sawEnemy", true);
        } else {
            animator.SetBool("sawEnemy", false);
        }
        if(AttackZone.DetectedObjects.Count > 0){
            animator.SetTrigger("Attack");
        } 
        if(canMove == true && AttackZone.DetectedObjects.Count > 0){
            LockMovement();
            animator.SetTrigger("Attack");
        } 

        if (canMove == true && detectionZone.detectedObjects.Count > 0){
            Vector2 direction = (detectionZone.detectedObjects[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

            
                animator.SetBool("sawEnemy", true);
        } else {
            animator.SetBool("sawEnemy", false);;
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
    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
    }

    public ParticleController particleController;
    void AttackParticle(){
        particleController.PlayAttackParticle();
    }
        
}
