using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArea : MonoBehaviour
{
    
    Animator animator;
    
    
public string TagTarget = "Player";
    public List<Collider2D> Detected = new List<Collider2D>();
    public Collider2D col;
    ChangeArea changeArea;
    
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
        
        animator = GetComponent<Animator>();
    }
   
    


    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == TagTarget){
            Detected.Add(collider);
            SwitchArea();
        }
        
    }
    void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.tag == TagTarget){
            Detected.Remove(collider);
            BackOn();
        }
    }
    
    public void SwitchArea(){
        animator.SetTrigger("SwitchArea");
    }
    public void BackOn(){
        animator.SetTrigger("BackOn");
    }
}
