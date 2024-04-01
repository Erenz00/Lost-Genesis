using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackZone : MonoBehaviour
{
    public string TagTarget;
    public List<Collider2D> DetectedObjects = new List<Collider2D>();
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == TagTarget){
            DetectedObjects.Add(collider);
        }
        
    }
    void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.tag == TagTarget){
            DetectedObjects.Remove(collider);
        }
    }
}