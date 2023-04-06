using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Vector2 size;
    private float left;
    private float right;
    private float top;
    private float bottom;

    void Start()
    {
        left = transform.position.x - size.x / 2;
        right = transform.position.x + size.x / 2;
        top = transform.position.y + size.y / 2;
        bottom = transform.position.y - size.y / 2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ResetBall(other.transform);
        }
    }

    private void ResetBall(Transform ballTransform)
    {
        // Reset ball position and velocity
        ballTransform.position = new Vector3(Random.Range(left, right), Random.Range(bottom, top));
        Rigidbody2D ballRigidbody = ballTransform.GetComponent<Rigidbody2D>();
        ballRigidbody.velocity = Vector2.zero;
        ballRigidbody.angularVelocity = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        left = transform.position.x - size.x / 2;
        right = transform.position.x + size.x / 2;
        top = transform.position.y + size.y / 2;
        bottom = transform.position.y - size.y / 2;

        Gizmos.DrawLine(new Vector3(left, top), new Vector3(right, top));
        Gizmos.DrawLine(new Vector3(left, bottom), new Vector3(right, bottom));
        Gizmos.DrawLine(new Vector3(left, top), new Vector3(left, bottom));
        Gizmos.DrawLine(new Vector3(right, top), new Vector3(right, bottom));
    }
}
