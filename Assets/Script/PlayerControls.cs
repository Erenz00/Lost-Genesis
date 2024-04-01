using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    bool IsMoving{
        set{
            isMoving = value;
        }
    }
    public attackZone AttackZone;
    public Vector2 movementInput;
    public float idleFriction = 2f;
    public float moveSpeed = 10f;
    public float maxSpeed = 80f;
    public Vector2 forceToApply;
    public float forceDamping;
   
    public float moveSpeed2 = 10f;
    
   
    
    Rigidbody2D rb;
    SpriteRenderer SpriteRenderer;
    Animator animator;
    bool canMove = true;
    bool isMoving = false;
   
    direction Direction;

    public FixedJoystick joystick;

    // attack code
     int numAttack = 1;
    
    void OnFire(){
        
        if (numAttack == 1){
            animator.SetTrigger("SwordAttack");
        } 
        
        else if (numAttack == 2){
            animator.SetTrigger("SwordAttack2");
            numAttack -= 2;
        } 
        numAttack += 1;
        
    }
    

    public void EndAttack(){
        UnlockMovement();
    }
    
    
        
    // end attack code
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Update(){
        movementInput.x = joystick.Horizontal;
        movementInput.y = joystick.Vertical;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {   
       


        
        //for pc vvvvvvvvvvv
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        // Make this a comment this for joystick to work ^^^^^^^^^


       
        if (canMove == true && movementInput != Vector2.zero){
            Vector2 moveForce = movementInput * moveSpeed;
            moveForce += forceToApply;
            forceToApply /= forceDamping;
            if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f){
                forceToApply = Vector2.zero;
            }

            //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);  
            
            
            rb.AddForce(movementInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
             
            if (rb.velocity.magnitude > maxSpeed){
                
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }

            if(movementInput.x < 0){
                SpriteRenderer.flipX = true;
                //gameObject.BroadcastMessage("IsFacingRight", false);
            } else if (movementInput.x > 0){
                //gameObject.BroadcastMessage("IsFacingRight", true);
                SpriteRenderer.flipX = false;

            }
            isMoving = true;
        } else {
           
            isMoving = false;
        }
        UpdateAnimationParameters();
    }
        
    

     
     
    

    public void LockMovement(){
        canMove = false;
    }
    public void UnlockMovement(){
        canMove = true;
    }

    void UpdateAnimationParameters(){
        if(movementInput.y > 0){
            animator.SetBool("IsMovingUp", isMoving);
            //gameObject.BroadcastMessage("IsFacingUp", true);
        }
        if (movementInput.y <= 0){
            animator.SetBool("isMoving", isMoving);
            animator.SetBool("IsMovingUp", false);
            //gameObject.BroadcastMessage("IsFacingUp", false);
        }
    }
    public float dashSpeed = 8000;
    public float AttackdashSpeed = 8000;
    public float StabdashSpeed = 8000;
    public void Dash(){
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.AddForce(movementInput * dashSpeed * Time.deltaTime);
    }
    public void attackdash(){
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.AddForce(movementInput * AttackdashSpeed * Time.deltaTime);
    }
    public void longAttackdash(){
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.AddForce(movementInput * StabdashSpeed * Time.deltaTime);
    }

    public void OnDash(){
        animator.SetTrigger("Dash");
    }
    
}



