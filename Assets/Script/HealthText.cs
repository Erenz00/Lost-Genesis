using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 300f;
    public Vector3 floatDirection = new Vector3(0, 1, 0);

    public TextMeshProUGUI textMesh;
    RectTransform rTransform;
    float timeElapsed = 0.0f;
    Color startingColor;
    // Update is called once per frame
    void Start()
    {
        
        rTransform = GetComponent<RectTransform>();
        startingColor = textMesh.color;
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;

        rTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 -(timeElapsed / timeToLive));

        if (timeElapsed > timeToLive){
            Destroy(gameObject);
        }
    }
}
