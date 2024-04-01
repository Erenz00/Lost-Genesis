using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SRockMob : MonoBehaviour
{
  
   
    public float damage = 1f;
    public float KnockBackForce = 1f;
   
    public DetectionZone detectionZone;
    public attackZone AttackZone;
    public float moveSpeed = 50f;
    Rigidbody2D rb;
    SpriteRenderer SpriteRenderer;
    bool canMove = false;
    
    Animator animator;
    void OnCollisionEnter2D(Collision2D col){
        
        IDamageable damageable = col.collider.GetComponent<IDamageable>();
        if(damageable != null){
            
            animator.SetTrigger("Attack");
            if(damageable != null){
            Vector2 direction = (col.collider.transform.position - transform.position).normalized;
            Vector2 Knockback = direction * KnockBackForce;
            damageable.OnHit(damage, Knockback);
            }
        }
    }
  

    void Start(){
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void FixedUpdate(){
        if(detectionZone.detectedObjects.Count > 0){
            animator.SetTrigger("sawEnemy");
        } else {
            animator.SetTrigger("loseEnemy");
        }
        if(canMove == true && AttackZone.DetectedObjects.Count > 0){
            LockMovement();
            animator.SetTrigger("Attack");
        } 

        if (canMove == true && detectionZone.detectedObjects.Count > 0){
            Vector2 direction = (detectionZone.detectedObjects[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

            if (direction != Vector2.zero){
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);  
            
            if(direction.x < 0) {
                SpriteRenderer.flipX = true;       
            } else if (direction.x > 0) {       
                SpriteRenderer.flipX = false;
            }
            }
                animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }
    

    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
    }
    
    public float GetBack = 1;
    void getBack(){
        Vector2 direction = (detectionZone.detectedObjects[0].transform.position + transform.position).normalized;
        rb.AddForce(direction * GetBack * Time.deltaTime);
    }   

    public ParticleController particleController;
    void DeathParticle(){
        particleController.PlayDeathParticle();
    }
    void HitParticle(){
        particleController.PlayHitParticle();
    }
}