using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force = 100f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Rigidbody2D physics;
    private Vector2 defaultBallPosition;
    

    void Awake(){
        physics = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        physics.isKinematic = true;
        defaultBallPosition = transform.position;
    }

   
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
           
            startPosition = getMousePosition();
            Debug.Log(startPosition);
        }
         if(Input.GetMouseButtonUp(0)){
                endPosition = getMousePosition();

             Vector2 power = startPosition - endPosition;
            physics.isKinematic = false;

             physics.AddForce(power * force, ForceMode2D.Force);
        }
        
    }

    void onCollisionEnter2D(Collision2D collision){
        physics.isKinematic = true;
        transform.position = defaultBallPosition;
        physics.velocity = Vector2.zero;
        physics.angularVelocity = 0f;
    }

    private Vector2 getMousePosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
