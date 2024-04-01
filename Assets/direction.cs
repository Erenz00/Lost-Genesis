using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class direction : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    [SerializeField]
    public Vector2 movementInput;
    public FixedJoystick joystick;
    public float rotationSpeed;
    [SerializeField]
    private void Update(){
        movementInput.x = joystick.Horizontal;
        movementInput.y = joystick.Vertical;
    }
    public bool canRotate = true;
    void FixedUpdate(){
        //for pc vvvv
        
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        // end for pc
        
        Vector2 Pointer = new Vector2(movementInput.x, movementInput.y).normalized; 
        float inputMagnitude = Mathf.Clamp01(Pointer.magnitude);
        if (canRotate == true && Pointer != Vector2.zero){
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, Pointer);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, (rotationSpeed * 2) * Time.deltaTime);
        }
        

    }
    public void LockRotation(){
        canRotate = false;
    }
    public void UnlockRotation(){
        canRotate = true;
    }
}
