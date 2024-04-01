using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistHitbox : MonoBehaviour
{   
    PlayerControls playerControls;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public float KnockBackForce = 5f;
    public float fistDamage = 2f; // The damage that the player's fists 
    public Collider2D fistCollider;
    public Vector3 faceRight = new Vector3(0.85f, -0.11f, 0);
    public Vector3 faceLeft = new Vector3(-0.85f, -0.11f, 0);
    public Vector3 faceUp = new Vector3(-0.85f, -0.11f, 0);

    public float health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool Targetable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    attackZone AttackZone;
    void Start()
    {
        if(fistCollider == null){
            Debug.LogWarning("Fist Collider not set");
        }
    }

    
    public float pushedBack = 1f;
    

    void OnTriggerEnter2D(Collider2D Collider){
    IDamageable damageInObject = Collider.GetComponent<IDamageable>();  
        Vector2 direction = (Vector2) (Collider.transform.position - transform.position).normalized;
        if (damageInObject != null){
        
        
        Vector2 Knockback = direction * KnockBackForce;
        damageInObject.OnHit(fistDamage, Knockback);
     
        } 
        
    }

    /*void IsFacingRight(bool isFacingRight){
        if(isFacingRight){
            gameObject.transform.localPosition = faceRight;
        } else {
            gameObject.transform.localPosition = faceLeft;
        }
    }*/    
    
    /*void IsFacingUp(bool isFacingUp){
        if(isFacingUp){
            gameObject.transform.localPosition = faceUp;
        }
        
    }*/

  
}
