using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class damageableCharacter : MonoBehaviour, IDamageable
{
    private float invincibleTimeElapsed = 0f;

    public bool disableSimulation = false;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    SpriteRenderer spriteRenderer;

    public bool canTurnInvincible = false;
    public float invincibilityTime = 0.25f;
    
    public float MaxRegen = 2;
    float RegenCount = 0;
    public float Health {
        set{
            
            if (value < _health){
                animator.SetTrigger("hit");
                HitParticle();
            }
            _health = value;
            if(_health <= 0){
                animator.SetBool("isAlive", false);
                DeathParticle();
                physicsCollider = GetComponent<Collider2D>();
            }
        }
        get {
            return _health;
        }

    }

    public float health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool Targetable { get {
            return _targetable;
        } 
        set {
            _targetable = value; 
            if(disableSimulation){
               rb.simulated = false; 
            }

            
        }
    }

    public bool Invincible { 
        get { 
            return _invincible;
        } 
        set {
            _invincible = value;
            if (Invincible == true){
                invincibleTimeElapsed = 0f;
            }
        }
    }

    private bool _invincible = false;
    private bool _targetable = true;
    public float _health = 3;
    // Start is called before the first frame update
    

    void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Destroy(){
        Destroy(gameObject);
    }

   

    
    public void OnHit(float damage, Vector2 knockback)
    {
        if(!Invincible){
            Health -= damage;
            RegenCount -=1;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        if (canTurnInvincible){
            Invincible = true;
        }
        if (_health <= 0){
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        
    }
    public void Regen(){
        if(RegenCount < MaxRegen){
            _health += 1;
            RegenCount +=1;
        }
    }

    public void OnHit(float damage)
    {
        
        if(!Invincible){
            Health -= damage;
        }
        if (canTurnInvincible){
            Invincible = true;
        }
    }
    public void FixedUpdate(){
        if (Invincible){
            invincibleTimeElapsed += Time.deltaTime;

            if(invincibleTimeElapsed > invincibilityTime){
                Invincible = false;
            }

        }
    }
    void beInvincible(){
        Invincible = true;
    }
    void RemoveInvincible(){
        Invincible = false;
    }
    public ParticleController particleController;
    void DeathParticle(){
        particleController.PlayDeathParticle();
    }
    void HitParticle(){
        particleController.PlayHitParticle();
    }

    
}
