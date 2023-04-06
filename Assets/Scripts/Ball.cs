using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    public float force = 100f;
    public int maxTrajectoryIteration = 50;
    public GameObject ballPrediction;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Rigidbody2D physics;
    private Vector2 defaultBallPosition;
    private Scene sceneMain;
    private PhysicsScene2D sceneMainPhysics;
    private Scene scenePrediction;
    private PhysicsScene2D scenePredictionPhysics;
    private float ballScorePosition;
    public UnityEvent scoredEvent;
    public UnityEvent <Transform> onGroundEvent;

    void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        physics.isKinematic = true;
        defaultBallPosition = transform.position;

        createSceneMain();
        createScenePrediction();
       
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = getMousePosition();
        }

        if (Input.GetMouseButton(0))
        {
            GameObject newBallPrediction = spawnBallPrediction();
            throwBall(newBallPrediction.GetComponent<Rigidbody2D>());

            createTrajectory(newBallPrediction);
    
            Destroy(newBallPrediction);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<LineRenderer>().positionCount = 0;
            physics.isKinematic = false;

            throwBall(physics);
        }
    }

     void FixedUpdate()
    {
        if (!sceneMainPhysics.IsValid()) return;

        scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        checkGroundContact(collision);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        ballScorePosition = transform.position.y;
    }

   

    private void createTrajectory(GameObject newBallPrediction)
    {
         LineRenderer ballLine = GetComponent<LineRenderer>();
         ballLine.positionCount = maxTrajectoryIteration;

        for (int i = 0; i < maxTrajectoryIteration; i++)
        {
           scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
           ballLine.SetPosition(i, new Vector3(newBallPrediction.transform.position.x, newBallPrediction.transform.position.y, 0));
        }
    }

    private void throwBall(Rigidbody2D physics)
    {
        physics.AddForce(getThrowPower(startPosition, endPosition), ForceMode2D.Force);
    }

    private GameObject spawnBallPrediction()
    {
        GameObject newBallPrediction = Instantiate(ballPrediction);
        SceneManager.MoveGameObjectToScene(newBallPrediction, scenePrediction);
        newBallPrediction.transform.position = transform.position;

        return newBallPrediction;
    }
    private Vector2 getThrowPower(Vector2 startPosition, Vector2 endPosition)
    {
        return (startPosition - endPosition) * force;
    }


   

    private Vector2 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



    private void createSceneMain()
    {
        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysics = sceneMain.GetPhysicsScene2D();
    }

    private void createScenePrediction()
    {
        CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        scenePrediction = SceneManager.CreateScene("PredictionScene", sceneParameters);
        scenePredictionPhysics = scenePrediction.GetPhysicsScene2D();
    }
    void OnTriggerExit2D(Collider2D collider)
{
    if(transform.position.y < ballScorePosition)
    {
        Debug.Log("Score");
        scoredEvent.Invoke();
    }

    // Reset ball position
    transform.position = defaultBallPosition;
    physics.velocity = Vector2.zero;
    physics.angularVelocity = 0f;
}

 private void checkGroundContact(Collision2D collision)
    {
        if(!collision.gameObject.tag.Equals("ground")) return;
        
        physics.isKinematic = true;
        physics.velocity = Vector2.zero;
        physics.angularVelocity = 0f;

        onGroundEvent.Invoke(transform);
    }
}
