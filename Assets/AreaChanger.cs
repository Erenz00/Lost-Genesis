using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChanger : MonoBehaviour
{
    public string TagTarget = "Player";
    public List<Collider2D> Detected = new List<Collider2D>();
    public Collider2D col;
    ChangeArea changeArea;
    
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
        
    }
    public int count;
    void awake(){
        count = Detected.Count;
    }
    


    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == TagTarget){
            Detected.Add(collider);
            
        }
        
    }
    void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.tag == TagTarget){
            Detected.Remove(collider);
        }
    }
    
    
}
