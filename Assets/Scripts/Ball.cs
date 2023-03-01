using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float force = 100f;
    public GameObject ballPrediction;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Rigidbody2D physics;
    private Vector2 defaultBallPosition;
    private Scene sceneMain;
    private PhysicsScene2D sceneMainPhysics;
    private Scene scenePrediction;
    private PhysicsScene2D scenePredictionPhysics;

    void Awake(){
        physics = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;

        physics.isKinematic = true;
        defaultBallPosition = transform.position;

        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysics = sceneMain.GetPhysicsScene2D();
        
        scenePrediction = SceneManager.CreateScene("PredictionScene");
        scenePredictionPhysics = scenePrediction.GetPhysicsScene2D();
    }

   
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
           
            startPosition = getMousePosition();
            Debug.Log(startPosition);
        }

        if(Input.GetMouseButton(0)){
           Vector2 dragPosition = getMousePosition();
           Vector2 power = startPosition - dragPosition;

           GameObject newBallPrediction = GameObject.Instantiate(ballPrediction);
           SceneManager.MoveGameObjectToScene(newBallPrediction, scenePrediction);
           newBallPrediction.transform.position = transform.position;

           newBallPrediction.GetComponent<Rigidbody2D>().AddForce(power * force, ForceMode2D.Force);

           LineRenderer ballLine = GetComponent<LineRenderer>();
           ballLine.positionCount = 50;

           for(int i=0; i<50; i++){
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
            ballLine.SetPosition(i, new Vector3(newBallPrediction.transform.position.x, newBallPrediction.transform.position.y, 0));
           }
             
            Destroy(newBallPrediction);

         if(Input.GetMouseButtonUp(0)){
                endPosition = getMousePosition();

            Vector2 power = startPosition - endPosition;
            physics.isKinematic = false;

             physics.AddForce(power * force, ForceMode2D.Force);
            
        }
        
        Destroy(newBallPrediction);
        }


}

    void FixedUpdate(){
         if(!sceneMainPhysics.IsValid()) return;
         
         scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
    }

    void onCollisionEnter2D(Collision2D collision)
    {
        physics.isKinematic = true;
        transform.position = defaultBallPosition;
        physics.velocity = Vector2.zero;
        physics.angularVelocity = 0f;
    }
    

    private Vector2 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);        
    }

    
}       
