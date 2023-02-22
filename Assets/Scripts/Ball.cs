using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    private Vector2 startPosition;
    private Vector2 endPosition;

    void Start()
    {
        
    }

   
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
           
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(startPosition);
        }
         if(Input.GetMouseButtonUp(0)){
           
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Debug.Log(endPosition);
        }
        
    }
}
